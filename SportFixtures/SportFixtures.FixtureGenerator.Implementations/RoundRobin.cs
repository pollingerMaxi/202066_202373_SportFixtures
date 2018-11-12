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
                EncountersTeams eTeam = new EncountersTeams() { Team = team, TeamId = team.Id };
                foreach (Team rival in teamList)
                {
                    EncountersTeams eRival = new EncountersTeams() { Team = rival, TeamId = rival.Id };
                    ICollection<EncountersTeams> opponents = new List<EncountersTeams>() { eTeam, eRival };
                    Encounter encounter = new Encounter() { Teams = opponents, SportId = team.SportId, Date = date };
                    while (encounterBL.TeamsHaveEncountersOnTheSameDay(encounter) || encounterBL.TeamsHaveEncountersOnTheSameDay(encounters, encounter))
                    {
                        encounter.Date = encounter.Date.AddDays(1);
                    }
                    encounters.Add(encounter);

                    Encounter encounter2 = new Encounter() { Teams = opponents, SportId = team.SportId, Date = date };
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
