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
    }
}