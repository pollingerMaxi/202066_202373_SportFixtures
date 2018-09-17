using System;
using SportFixtures.Data.Entities;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Repository;

namespace SportFixtures.BusinessLogic.Implementations
{
    public class SportBusinessLogic : ISportBusinessLogic
    {   
        private IUnitOfWork UnitOfWork;
        public SportBusinessLogic(IUnitOfWork UnitOfWork){
            this.UnitOfWork = UnitOfWork;
        }

        public bool UniqueName(string sportName){
            return true;
        }

        public void AddSport(string name)
        {
            if (UniqueName(name))
            {
                UnitOfWork.SportRepository.Insert(new Sport() { Name = name });
                UnitOfWork.Save();
            }
        }
    }
}
