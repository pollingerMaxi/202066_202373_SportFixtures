using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface IUserBusinessLogic : IDisposable
    {
        /// <summary>
        /// Adds a user to the system.
        /// </summary>
        /// <param name="user"></param>
        void AddUser(User user);

        /// <summary>
        /// Adds the given team to the user's Favorites.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="team"></param>
        void FollowTeam(int userId, int teamId);

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="user"></param>
        void Update(User user);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="user"></param>
        void Delete(int id);

        /// <summary>
        /// Throws exception if the given user is not in the system.
        /// </summary>
        /// <param name="user"></param>
        void CheckIfExists(int userId);

        /// <summary>
        /// Login a user, generates and returns the token.
        /// </summary>
        /// <param name="user"></param>
        Guid Login(User user);

        /// <summary>
        /// Returns all the users in the repository.
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Returns the user with the given ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        User GetById(int userId);

        /// <summary>
        /// If the given token is valid, returns the user with said token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        User TokenIsValid(string token);

        /// <summary>
        /// Logouts a user.
        /// </summary>
        /// <param name="email"></param>
        void Logout(string email);

        /// <summary>
        /// Returns the favorites of the given user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<UsersTeams> GetFavoritesOfUser(int userId);
    }
}
