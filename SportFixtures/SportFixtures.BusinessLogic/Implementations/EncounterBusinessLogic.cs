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
        }

        public void Update(Encounter encounter){
            CheckIfExists(encounter);
            Validate(encounter);
            repository.Update(encounter);
            repository.Save();
        }

        public void Delete(Encounter encounter){
            CheckIfExists(encounter);
            repository.Delete(encounter);
            repository.Save();
        }

        public void CheckIfExists(Encounter encounter){
            if (repository.GetById(encounter.Id) == null)
            {
                throw new EncounterDoesNotExistsException();
            }
        }

        private bool CheckIfTeamHasEncounterOnTheSameDay(Team team, DateTime date){
            return repository.Get().Any(e => ((e.Date.Date == date.Date) && (e.Team1.Equals(team) || e.Team2.Equals(team))));
        }

        public void CheckTeamsEncountersDate(Encounter encounter){
            if(CheckIfTeamHasEncounterOnTheSameDay(encounter.Team1, encounter.Date) ||CheckIfTeamHasEncounterOnTheSameDay(encounter.Team2, encounter.Date)){
                throw new TeamAlreadyHasAnEncounterOnTheSameDayException();
            }
        }
    }
}