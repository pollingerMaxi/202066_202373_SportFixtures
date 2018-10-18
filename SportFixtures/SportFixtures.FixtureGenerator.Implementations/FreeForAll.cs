using System;
using SportFixtures.FixtureGenerator;
using System.Collections;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using SportFixtures.Exceptions.EncounterExceptions;

namespace SportFixtures.FixtureGenerator.Implementations
{
    public class FreeForAll : IFixtureGenerator
    {
        private IEncounterBusinessLogic encounterBL;

        public FreeForAll(IEncounterBusinessLogic encounterBL)
        {
            this.encounterBL = encounterBL;
        }
        public ICollection<Encounter> GenerateFixture(IEnumerable<Team> teams, DateTime date)
        {
            if (teams.Count() < 2)
            {
                throw new NotEnoughTeamsForEncounterException("Not enough teams to create free for all encounters.");
            }

            List<Team> teamList = teams.ToList();
            ICollection<Encounter> generatedEncounters = new List<Encounter>();
            foreach (Team team in teams)
            {
                teamList.Remove(team);
                foreach (Team rival in teamList)
                {
                    ICollection<Team> opponents = new List<Team>() { team, rival };
                    Encounter encounter = new Encounter() { Teams = opponents, SportId = team.SportId, Date = date };
                    while (encounterBL.TeamsHaveEncountersOnTheSameDay(encounter) || encounterBL.TeamsHaveEncountersOnTheSameDay(generatedEncounters, encounter))
                    {
                        encounter.Date = encounter.Date.AddDays(1);
                    }
                    generatedEncounters.Add(encounter);
                }
            }
            return generatedEncounters;
        }
    }
}