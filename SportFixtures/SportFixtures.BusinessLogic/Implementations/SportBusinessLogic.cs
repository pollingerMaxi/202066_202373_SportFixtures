using System;
using SportFixtures.Data.Entities;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Repository;

namespace SportFixtures.BusinessLogic.Implementations
{
    public class SportBusinessLogic : ISportBusinessLogic
    {   
        private IRepository<Sport> repository;

        public SportBusinessLogic(IRepository<Sport> repository){
            this.repository = repository;
        }

        public bool UniqueName(string sportName){
            return true;
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
