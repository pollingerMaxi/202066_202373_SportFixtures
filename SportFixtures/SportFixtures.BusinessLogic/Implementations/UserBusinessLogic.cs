using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.UserExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SportFixtures.BusinessLogic.Implementations
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private IRepository<User> repository;
        private ITeamBusinessLogic teamBusinessLogic;

        public UserBusinessLogic(IRepository<User> repository, ITeamBusinessLogic teamBL)
        {
            this.repository = repository;
            this.teamBusinessLogic = teamBL;
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

            if (!UsernameIsUnique(user.Username))
            {
                throw new UsernameAlreadyInUseException();
            }
        }

        private bool ValidateEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        private void CheckIfUserExists(User user)
        {
            if (repository.GetById(user.Id) == null)
            {
                throw new UserDoesNotExistException();
            }
        }

        public void FollowTeam(User user, Team team)
        {
            teamBusinessLogic.CheckIfTeamExists(team);
            CheckIfUserExists(user);
            user.FollowedTeams.Add(team);
            repository.Save();
        }

        public void Update(User user)
        {
            CheckIfUserExists(user);
            repository.Update(user);
            repository.Save();
        }

        private bool UsernameIsUnique(string username)
        {
            return repository.Get(null, null, "").FirstOrDefault(u => u.Username == username) == null;
        }

        public void Delete(User user)
        {
            CheckIfUserExists(user);
            repository.Delete(user);
            repository.Save();
        }
    }
}
