using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Enums;
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
        private IRepository<UsersTeams> favoritesRepository;
        private ITeamBusinessLogic teamBusinessLogic;

        public UserBusinessLogic(IRepository<User> repository, ITeamBusinessLogic teamBL, IRepository<UsersTeams> favsRepo)
        {
            this.repository = repository;
            favoritesRepository = favsRepo;
            this.teamBusinessLogic = teamBL;
        }

        public void AddUser(User user)
        {
            ValidateUser(user);
            user.Token = Guid.NewGuid();
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

            if (!UsernameIsUnique(user))
            {
                throw new UsernameAlreadyInUseException();
            }

            if (!EmailIsUnique(user))
            {
                throw new EmailAlreadyRegisteredException();
            }
        }

        private bool EmailIsUnique(User user)
        {
            return repository.Get(u => u.Id != user.Id, null, "").FirstOrDefault(u => u.Email == user.Email) == null;
        }

        /// <summary>
        /// Validates with a regex that the string passed satisfies the basic email rules (alphanumeric@alphanumeric.com).
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool ValidateEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public void CheckIfExists(int userId)
        {
            if (repository.GetById(userId) == null)
            {
                throw new UserDoesNotExistException();
            }
        }

        public void FollowTeam(int userId, int teamId)
        {
            teamBusinessLogic.CheckIfExists(teamId);
            CheckIfExists(userId);
            favoritesRepository.Insert(new UsersTeams() { UserId = userId, TeamId = teamId });
            favoritesRepository.Save();
        }

        public void Update(User user)
        {
            ValidateUser(user);
            User dbUser = GetById(user.Id);
            dbUser.Name = user.Name;
            dbUser.LastName = user.LastName;
            dbUser.Username = user.Username;
            dbUser.Email = user.Email;
            dbUser.Role = user.Role;
            dbUser.Password = user.Password;
            repository.Update(dbUser);
            repository.Save();
        }

        private bool UsernameIsUnique(User user)
        {
            return repository.Get(u => u.Id != user.Id, null, "").FirstOrDefault(u => u.Username == user.Username) == null;
        }

        public void Delete(int id)
        {
            CheckIfExists(id);
            repository.Delete(id);
            repository.Save();
        }

        public User Login(User user)
        {
            var users = repository.Get(u => u.Username == user.Username, null, "");
            if (users.Count() == 0)
            {
                throw new UserDoesNotExistException();
            }

            var userFromDb = users.FirstOrDefault();
            if (!user.Username.Equals(userFromDb.Username) || !user.Password.Equals(userFromDb.Password))
            {
                throw new EmailOrPasswordException();
            }

            var token = GenerateToken(user, userFromDb);
            return userFromDb;
        }

        /// <summary>
        /// Updates LoggedUser and user's token.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userFromDb"></param>
        private Guid GenerateToken(User user, User userFromDb)
        {
            var token = Guid.NewGuid();
            userFromDb.Token = token;
            UpdateUserToken(userFromDb);
            return token;
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

        public IEnumerable<User> GetAll()
        {
            return repository.Get(null, null, "");
        }

        public User GetById(int userId)
        {
            return repository.GetById(userId) ?? throw new UserDoesNotExistException();
        }

        public User TokenIsValid(string token)
        {
            var tkn = Guid.Parse(token);
            return repository.Get(null, null, "").FirstOrDefault(u => u.Token == tkn);
        }

        public void Dispose()
        {
            repository.Dispose();
        }

        public void Logout(string username)
        {
            var userFromDb = repository.Get(u => u.Username == username, null, "").FirstOrDefault();
            if (userFromDb == null)
            {
                throw new UserDoesNotExistException();
            }

            if (userFromDb.Token == null)
            {
                throw new UserIsNotLoggedInException();
            }

            LogoutUser(userFromDb);
        }

        private void LogoutUser(User user)
        {
            user.Token = null;
            repository.Update(user);
            repository.Save();
        }

        public IEnumerable<UsersTeams> GetFavoritesOfUser(int userId)
        {
            CheckIfExists(userId);
            return favoritesRepository.Get(f => f.UserId == userId, null, "Team");
        }
    }
}
