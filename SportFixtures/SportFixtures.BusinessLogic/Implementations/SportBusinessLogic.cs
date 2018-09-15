using System;
using SportFixtures.Data.Entities;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Repository;

namespace SportFixtures.BusinessLogic.Implementations
{
    public class SportBusinessLogic : ISportBusinessLogic
    {   
        private UnitOfWork UnitOfWork;
        public SportBusinessLogic(UnitOfWork UnitOfWork){
            this.UnitOfWork = UnitOfWork;
        }
        public bool UniqueName(string sportName){
            return true;
        }
    }
}
