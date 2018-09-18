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

        public bool UniqueName(string sportName)
        {
            return !repository.Get().Any(s => s.Name == sportName) ? true : throw new DuplicatedSportNameException();
        }

        public void AddSport(string name)
        {
            if (UniqueName(name))
            {
                repository.Insert(new Sport() { Name = name });
                repository.Save();
            }
        }
    }
}
