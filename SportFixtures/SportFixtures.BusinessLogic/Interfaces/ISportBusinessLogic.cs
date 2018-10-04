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
        void Delete(int id);
        Sport GetById(int id);
        IEnumerable<Sport> GetAll();
    }
}