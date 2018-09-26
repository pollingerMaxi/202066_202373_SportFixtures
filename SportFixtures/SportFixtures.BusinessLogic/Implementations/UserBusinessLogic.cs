using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.BusinessLogic.Implementations
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private IRepository<User> repository;

        public UserBusinessLogic(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public void AddUser(User user)
        {
            repository.Insert(user);
            repository.Save();
        }
    }
}
