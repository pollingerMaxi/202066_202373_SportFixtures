using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Exceptions.EncounterExceptions;

namespace SportFixtures.FixtureGenerator.Implementations
{
    public class RoundRobin : IFixtureGenerator
    {
        private IEncounterBusinessLogic encounterBL;

        public RoundRobin(IEncounterBusinessLogic encounterBL)
        {
            this.encounterBL = encounterBL;
        }

        public ICollection<Encounter> GenerateFixture(IEnumerable<Team> teams, DateTime date)
        {
            if (teams.Count() < 2)
            {
                throw new NotEnoughTeamsForEncounterException("Not enough teams to create round robin encounters.");
            }

            DateTime newDate = date.AddDays(1);
            List<Team> teamList = teams.ToList();
            ICollection<Encounter> encounters = new List<Encounter>();
            foreach (Team team in teams)
            {
                teamList.Remove(team);
                foreach (Team rival in teamList)
                {
                    Encounter encounter = new Encounter() { Team1 = team, Team2 = rival, SportId = team.SportId, Date = date };
                    while (encounterBL.TeamsHaveEncountersOnTheSameDay(encounter) || encounterBL.TeamsHaveEncountersOnTheSameDay(encounters, encounter))
                    {
                        encounter.Date = encounter.Date.AddDays(1);
                    }
                    encounters.Add(encounter);

                    Encounter encounter2 = new Encounter() { Team1 = rival, Team2 = team, SportId = team.SportId, Date = date };
                    while (encounterBL.TeamsHaveEncountersOnTheSameDay(encounter2) || encounterBL.TeamsHaveEncountersOnTheSameDay(encounters, encounter2))
                    {
                        encounter2.Date = encounter2.Date.AddDays(1);
                    }
                    encounters.Add(encounter2);
                }
            }
            return encounters;
        }
    }
}
