using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface ISportBusinessLogic
    {
        void Add(Sport sport);
        void AddTeamToSport(Team team);
        void Update(Sport sport);
        void Delete(Sport sport);
        IEnumerable<Sport> GetAll();
    }
}