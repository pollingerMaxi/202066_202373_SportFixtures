using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data;
using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportFixtures.BusinessLogic.Implementations
{
    public class PositionTableCalculator : IPositionTableCalculator
    {
        private ISportBusinessLogic sportBL;
        private IEncounterBusinessLogic encounterBL;

        public PositionTableCalculator(ISportBusinessLogic sportBL, IEncounterBusinessLogic encounterBL)
        {
            this.sportBL = sportBL;
            this.encounterBL = encounterBL;
        }

        public ICollection<Score> GeneratePositionTableForSport(int sportId)
        {
            ICollection<Score> positions = new List<Score>();
            Sport sport = sportBL.GetById(sportId);
            List<Team> teams = sport.Teams.ToList();
            foreach (Team team in teams)
            {
                List<Encounter> encounters = encounterBL.GetAllEncountersOfTeam(team.Id).ToList();
                foreach (Encounter encounter in encounters)
                {
                    Score position = new Score() { Team = team };
                    int teamPosition = encounter.Results.First(t => t.TeamId == team.Id).Position;
                    if (sport.EncounterMode == EncounterMode.Double)
                    {
                        if (teamPosition == 2)
                        {
                            position.Points = 3;
                        }
                        else if (teamPosition == 1)
                        {
                            position.Points = 1;
                        }
                        else
                        {
                            position.Points = 0;
                        }
                    }
                    else if (sport.EncounterMode == EncounterMode.Multiple)
                    {
                        if (teamPosition == 1)
                        {
                            position.Points = 3;
                        }
                        else if (teamPosition == 2)
                        {
                            position.Points = 2;
                        }
                        else if (teamPosition == 3)
                        {
                            position.Points = 1;
                        }
                        else
                        {
                            position.Points = 0;
                        }
                    }
                    positions.Add(position);
                }
            }
            return positions;
        }
    }
}