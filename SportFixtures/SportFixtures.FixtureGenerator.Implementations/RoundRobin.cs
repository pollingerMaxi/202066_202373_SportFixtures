using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;

namespace SportFixtures.FixtureGenerator.Implementations
{
    public class RoundRobin : IFixtureGenerator
    {
        private IEncounterBusinessLogic encounterBL;

        public RoundRobin(IEncounterBusinessLogic encounterBL)
        {
            this.encounterBL = encounterBL;
        }

        public ICollection<Encounter> GenerateFixture(IEnumerable<Team> teams, DateTime date, int sportId)
        {
            DateTime newDate = date.AddDays(1);
            List<Team> teamList = teams.ToList();
            ICollection<Encounter> encounters = new List<Encounter>();
            foreach (Team team in teams)
            {
                teamList.Remove(team);
                foreach (Team rival in teamList)
                {
                    Encounter encounter = new Encounter() { Team1 = team, Team2 = rival, SportId = sportId, Date = date };
                    while (encounterBL.TeamsHaveEncountersOnTheSameDay(encounter) || encounterBL.TeamsHaveEncountersOnTheSameDay(encounters, encounter))
                    {
                        encounter.Date = encounter.Date.AddDays(1);
                    }
                    Encounter encounter2 = new Encounter() { Team1 = rival, Team2 = team, SportId = sportId, Date = date };
                    while (encounterBL.TeamsHaveEncountersOnTheSameDay(encounter) || encounterBL.TeamsHaveEncountersOnTheSameDay(encounters, encounter))
                    {
                        encounter2.Date = encounter2.Date.AddDays(1);
                    }
                    encounters.Add(encounter);
                    encounters.Add(encounter2);
                }
            }
            return encounters;
        }
    }
}
