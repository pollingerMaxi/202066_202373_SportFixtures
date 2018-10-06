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
                    DateTime date2 = date;
                    Encounter encounter = new Encounter(){ Team1 = team, Team2 = rival, SportId = sportId};
                    while(encounterBL.TeamsHaveEncountersOnTheSameDay(encounter)){
                        date.AddDays(1);
                    }
                    encounter.Date = date2;
                    encounters.Add(encounter);
                }
            }
            return encounters;
        }

    }
}