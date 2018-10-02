using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface IUserBusinessLogic
    {
        /// <summary>
        /// Adds a user to the system.
        /// </summary>
        /// <param name="user"></param>
        void AddUser(User user);

        /// <summary>
        /// Adds the given team to the user's FollowedTeams.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="team"></param>
        void FollowTeam(User user, Team team);

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="user"></param>
        void Update(User user);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="user"></param>
        void Delete(User user);

        /// <summary>
        /// Throws exception if the given user is not in the system.
        /// </summary>
        /// <param name="user"></param>
        void CheckIfExists(int userId);

        /// <summary>
        /// Login a user and generates the token.
        /// </summary>
        /// <param name="user"></param>
        void Login(User user);
        IEnumerable<User> GetAll();
    }
}
