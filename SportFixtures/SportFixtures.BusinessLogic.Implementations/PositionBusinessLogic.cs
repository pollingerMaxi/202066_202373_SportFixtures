using System;
using SportFixtures.Data.Entities;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using SportFixtures.Data;

namespace SportFixtures.BusinessLogic.Implementations
{
    public class PositionBusinessLogic : IPositionBusinessLogic
    {        
        private IRepository<Position> repository;
        private ISportBusinessLogic sportBL;
        private IEncounterBusinessLogic encounterBL;
        
        public PositionBusinessLogic(IRepository<Position> repository, ISportBusinessLogic sportBL, IEncounterBusinessLogic encounterBL){
            this.repository = repository;
            this.sportBL = sportBL;
            this.encounterBL = encounterBL;

        }
        public void Add(Position position)
        {
            repository.Attach(position);
            repository.Insert(position);
            repository.Save();
        }
        public void Update(Position position)
        {
            repository.Update(position);
            repository.Save();
        }
        public Position GetTeamPosition(int teamId)
        {
            return repository.Get(p => p.Team.Id == teamId, null, "Team").FirstOrDefault();
        }

        public void UpdatePositions(int sportId)
        {
            Sport sport = sportBL.GetById(sportId);
            List<Team> teams = sport.Teams.ToList();
            foreach(Team team in teams){
                Position position = GetTeamPosition(team.Id);
                List<Encounter> encounters = encounterBL.GetAllEncountersOfTeam(team.Id).ToList();
                foreach(Encounter encounter in encounters){
                    int teamPosition = encounter.Results.First(t => t.Id == team.Id).Position;
                    if(sport.EncounterMode == EncounterMode.Double){
                        if(position.points == 2){
                            position.points += 3;
                        }
                        else if(position.points == 1){
                            position.points += 1;
                        }
                    }else if(sport.EncounterMode == EncounterMode.Multiple){
                        if(position.points == 1){
                            position.points += 3;
                        }
                        else if(position.points == 2){
                            position.points += 2;
                        }else{
                            position.points += 1;
                        }
                    }
                    repository.Update(position);
                }
            }
        }
    }
}