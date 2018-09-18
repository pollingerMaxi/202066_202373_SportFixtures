using System;
using SportFixtures.Data.Entities;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Repository;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using SportFixtures.Exceptions.SportExceptions;

namespace SportFixtures.BusinessLogic.Implementations
{
    public class SportBusinessLogic : ISportBusinessLogic
    {
        private IRepository<Sport> repository;

        public SportBusinessLogic(IRepository<Sport> repository)
        {
            this.repository = repository;
        }

        private bool UniqueName(string sportName)
        {
            return !repository.Get().Any(s => s.Name == sportName);
        }

        public void ValidateSport(Sport sport)
        {
            if(!UniqueName(sport.Name))
            {
                throw new DuplicatedSportNameException();
            }
        }

        public void AddSport(Sport sport)
        {
            ValidateSport(sport);
            repository.Insert(sport);
            repository.Save();
            
        }
    }
}
