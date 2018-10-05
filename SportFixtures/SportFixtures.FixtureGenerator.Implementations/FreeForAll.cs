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
        public ICollection<Encounter> GenerateFixture(ICollection<Team> teams, DateTime date)
        {
            ICollection<Encounter> encounters = new List<Encounter>();
            int teamsCount = teams.Count();
            int lastTeam = 0;
            //for(int i = 0; i++; i == teamsCount){
            //    team = teams[i];
            //    for(lastTeam; i++; i == teamsCount){

            //        Encounter encounter = new Encounter(){};
            //    }

            //}
            return null;
        }
    }
}