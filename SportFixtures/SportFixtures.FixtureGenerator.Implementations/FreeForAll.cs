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
        public ICollection<Encounter> GenerateFixture(IEnumerable<Team> teams, DateTime date)
        {
            List<Team> teamList = teams.ToList();
            ICollection<Encounter> generatedEncounters = new List<Encounter>();
            foreach(Team team in teams){
                teamList.Remove(team);
                foreach(Team rival in teamList){
                    Encounter encounter = new Encounter(){ Team1 = team, Team2 = rival, SportId = team.SportId, Date = date};
                    while(encounterBL.TeamsHaveEncountersOnTheSameDay(encounter) || encounterBL.TeamsHaveEncountersOnTheSameDay(generatedEncounters, encounter)){
                        encounter.Date = encounter.Date.AddDays(1);
                    }
                    generatedEncounters.Add(encounter);
                }
            }
            return generatedEncounters;
        }
    }
}