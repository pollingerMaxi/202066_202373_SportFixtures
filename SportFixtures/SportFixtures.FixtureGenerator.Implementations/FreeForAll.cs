using System;
using SportFixtures.FixtureGenerator;
using System.Collections;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using SportFixtures.Exceptions.EncounterExceptions;
using SportFixtures.Data;

namespace SportFixtures.FixtureGenerator.Implementations
{
    public class FreeForAll : IFixtureGenerator
    {
        private IEncounterBusinessLogic encounterBL;
        public EncounterMode encounterMode { get; set; }

        public FreeForAll(IEncounterBusinessLogic encounterBL)
        {
            this.encounterBL = encounterBL;
            this.encounterMode = EncounterMode.Double;
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
                EncountersTeams eTeam = new EncountersTeams() { Team = team, TeamId = team.Id };
                foreach (Team rival in teamList)
                {
                    EncountersTeams eRival = new EncountersTeams() { Team = rival, TeamId = rival.Id };
                    ICollection<EncountersTeams> opponents = new List<EncountersTeams>() { eTeam, eRival };
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