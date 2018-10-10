using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface ISportBusinessLogic
    {
        /// <summary>
        /// Add a sport to the system.
        /// </summary>
        /// <param name="sport"></param>
        void Add(Sport sport);

        /// <summary>
        /// Add a team to a sport.
        /// </summary>
        /// <param name="team"></param>
        void AddTeamToSport(Team team);

        /// <summary>
        /// Updates a sport.
        /// </summary>
        /// <param name="sport"></param>
        void Update(Sport sport);

        /// <summary>
        /// Deletes a sport.
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Returns the sport with given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Sport GetById(int id);

        /// <summary>
        /// Returns all the sports in the repository.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Sport> GetAll();
    }
}