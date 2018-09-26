using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.UserExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
            ValidateUser(user);
            repository.Insert(user);
            repository.Save();
        }

        private void ValidateUser(User user)
        {
            if (String.IsNullOrWhiteSpace(user.Name))
            {
                throw new InvalidUserNameException();
            }

            if (String.IsNullOrWhiteSpace(user.Username))
            {
                throw new InvalidUserUsernameException();
            }

            if (String.IsNullOrWhiteSpace(user.Email) || !ValidateEmail(user.Email))
            {
                throw new InvalidUserEmailException();
            }

            if (String.IsNullOrWhiteSpace(user.LastName))
            {
                throw new InvalidUserLastNameException();
            }
        }

        private bool ValidateEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
