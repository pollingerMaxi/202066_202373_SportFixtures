using System;
using SportFixtures.Data.Entities;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.EncounterExceptions;
using System.Collections.Generic;
using System.Linq;

namespace SportFixtures.BusinessLogic.Implementations
{
    public class EncounterBusinessLogic : IEncounterBusinessLogic
    {
        private IRepository<Encounter> repository;

        public EncounterBusinessLogic(IRepository<Encounter> repository)
        {
            this.repository = repository;
        }

        public void Add(Encounter encounter)
        {
            Validate(encounter);
            repository.Insert(encounter);
            repository.Save();
        }

        private void Validate(Encounter encounter)
        {
            if(encounter.Team1 == null || encounter.Team2 == null){
                throw new EncounterTeamsCantBeNullException();
            }
            if(encounter.Team1 == encounter.Team2){
                throw new EncounterSameTeamException();
            }
            if(encounter.Team1.SportId != encounter.Team2.SportId){
                throw new EncounterTeamsDifferentSportException();
            }
            if(encounter.SportId != encounter.Team1.SportId){
                throw new EncounterSportDifferentFromTeamsSportException();
            }
            CheckTeamsEncountersDate(encounter);
        }

        public void Update(Encounter encounter){
            CheckIfExists(encounter.Id);
            Validate(encounter);
            repository.Update(encounter);
            repository.Save();
        }

        public void Delete(Encounter encounter){
            CheckIfExists(encounter.Id);
            repository.Delete(encounter);
            repository.Save();
        }

        public void CheckIfExists(int encounterId){
            if (repository.GetById(encounterId) == null)
            {
                throw new EncounterDoesNotExistException();
            }
        }

        private bool CheckIfTeamHasEncounterOnTheSameDay(Team team, DateTime date, int encounterId){
            return repository.Get().Any(e => ((e.Id != encounterId) && (e.Date.Date == date.Date) && (e.Team1.Equals(team) || e.Team2.Equals(team))));
        }

        private void CheckTeamsEncountersDate(Encounter encounter){
            if(CheckIfTeamHasEncounterOnTheSameDay(encounter.Team1, encounter.Date, encounter.Id) 
                ||CheckIfTeamHasEncounterOnTheSameDay(encounter.Team2, encounter.Date, encounter.Id)){
                throw new TeamAlreadyHasAnEncounterOnTheSameDayException();
            }
        }

        public void AddCommentToEncounter(Comment comment){
            Encounter encounter = GetEncounterById(comment.EncounterId);
            encounter.Comments.Add(comment);
        }

        private Encounter GetEncounterById(int encounterId)
        {
            return repository.GetById(encounterId);
        }

        public IEnumerable<Encounter> GetAll()
        {
            return repository.Get(null, null, "");
        }
    }
}