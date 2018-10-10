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
        void Update(Sport sport);
        void Delete(int id);
        Sport GetById(int id);
        IEnumerable<Sport> GetAll();
    }
}