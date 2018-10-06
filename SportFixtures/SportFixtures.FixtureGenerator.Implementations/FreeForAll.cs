using System;
using SportFixtures.FixtureGenerator;
using System.Collections;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SportFixtures.FixtureGenerator.Implementations
{
    public class FreeForAll : IFixtureGenerator
    {
        private IEncounterBusinessLogic encounterBL;

        public FreeForAll(IEncounterBusinessLogic encounterBL){
            this.encounterBL = encounterBL;
        }
        public ICollection<Encounter> GenerateFixture(IEnumerable<Team> teams, DateTime date, int sportId)
        {
            DateTime newDate = date.AddDays(1);
            List<Team> teamList = teams.ToList();
            ICollection<Encounter> encounters = new List<Encounter>();
            foreach(Team team in teams){
                teamList.Remove(team);
                foreach(Team rival in teamList){
                    Encounter encounter = new Encounter(){ Team1 = team, Team2 = rival, SportId = sportId, Date = date};
                    while(encounterBL.TeamsHaveEncountersOnTheSameDay(encounter) || TeamsHaveEncountersOnTheSameDay(encounters, encounter)){
                        encounter.Date = encounter.Date.AddDays(1);
                    }
                    encounters.Add(encounter);
                }
            }
            return encounters;
        }

        private bool TeamsHaveEncountersOnTheSameDay(ICollection<Encounter> encounters, Encounter encounter){
             return (encounters.Any(e => ((e.Date.Date == encounter.Date.Date) && (e.Team1.Equals(encounter.Team1) || e.Team2.Equals(encounter.Team1))))
                ||  encounters.Any(e => ((e.Date.Date == encounter.Date.Date) && (e.Team1.Equals(encounter.Team2) || e.Team2.Equals(encounter.Team2)))));
            
        }

    }
}