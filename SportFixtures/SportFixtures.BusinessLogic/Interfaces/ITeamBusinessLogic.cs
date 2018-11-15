using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;
using SportFixtures.Data.DTOs;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface ITeamBusinessLogic
    {
        /// <summary>
        /// Adds a team to the system.
        /// </summary>
        /// <param name="team"></param>
        void Add(Team team);

        /// <summary>
        /// Updates a team.
        /// </summary>
        /// <param name="team"></param>
        void Update(Team team);
        
        /// <summary>
        /// Deletes a team.
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Returns exception if team does not exist.
        /// </summary>
        /// <param name="teamId"></param>
        void CheckIfExists(int teamId);

        /// <summary>
        /// Returns all teams in repository.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Team> GetAll(TeamFilterDTO filter);

        /// <summary>
        /// Returns the team with given ID.
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        Team GetById(int teamId);
    }
}