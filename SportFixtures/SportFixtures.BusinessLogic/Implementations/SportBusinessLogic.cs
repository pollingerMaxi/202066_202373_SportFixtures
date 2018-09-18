using System;
using SportFixtures.Data.Entities;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Repository;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

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
            return !repository.Get().Any(s => s.Name == sportName);
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
