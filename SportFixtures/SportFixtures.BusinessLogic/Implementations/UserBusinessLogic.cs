using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data;
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

        public User LoggedUser { get; private set; }

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

        /// <summary>
        /// Validates business rules for a user.
        /// </summary>
        /// <param name="user"></param>
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

        /// <summary>
        /// Validates with a regex that the string passed satisfies the basic email rules (alphanumeric@alphanumeric.com).
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool ValidateEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// Throws exception if the given user is not in the system.
        /// </summary>
        /// <param name="user"></param>
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
            CheckIfLoggedUserIsAdmin();
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

        /// <summary>
        /// Throws exception if LoggedUser is not an admin.
        /// </summary>
        private void CheckIfLoggedUserIsAdmin()
        {
            if (LoggedUser.Role != Role.Admin)
            {
                throw new LoggedUserIsNotAdminException();
            }
        }

        public void Login(User user)
        {
            CheckIfUserExists(user);
            var userFromDb = repository.GetById(user.Id);
            if (!user.Email.Equals(userFromDb.Email) && !user.Password.Equals(userFromDb.Password))
            {
                throw new EmailOrPasswordException();
            }
            UpdateUsers(user, userFromDb);
        }

        /// <summary>
        /// Updates LoggedUser and user's token.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userFromDb"></param>
        private void UpdateUsers(User user, User userFromDb)
        {
            LoggedUser = user;
            userFromDb.Token = new Guid();
            UpdateUserToken(userFromDb);
        }

        /// <summary>
        /// If we call this method means that we don't need to validate if logged user is admin
        /// because this should only be called from the Login.
        /// Also, user existance was already checked => we can skip checking again.
        /// </summary>
        /// <param name="user"></param>
        private void UpdateUserToken(User user)
        {
            repository.Update(user);
            repository.Save();
        }
    }
}
