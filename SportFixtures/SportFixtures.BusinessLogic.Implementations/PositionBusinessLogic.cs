using System;
using SportFixtures.Data.Entities;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.PositionExceptions;
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
            CheckIfExists(position);
            repository.Attach(position);
            repository.Insert(position);
            repository.Save();
        }

        private void CheckIfExists(Position position)
        {
            if(repository.Get(p => ((p.Id != position.Id) && (p.Team.Id == position.Id)), null, "Team").Any()){
                throw new TeamPositionAlreadyExistsExeption();
            }
        }

        public void Update(Position position)
        {
            CheckIfExists(position);
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
                        if(position.Points == 2){
                            position.Points += 3;
                        }
                        else if(position.Points == 1){
                            position.Points += 1;
                        }
                    }else if(sport.EncounterMode == EncounterMode.Multiple){
                        if(position.Points == 1){
                            position.Points += 3;
                        }
                        else if(position.Points == 2){
                            position.Points += 2;
                        }else{
                            position.Points += 1;
                        }
                    }
                    repository.Update(position);
                }
            }
        }
    }
}