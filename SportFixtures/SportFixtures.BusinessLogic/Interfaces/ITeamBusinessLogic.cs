using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface ITeamBusinessLogic
    {
        void Add(Team team);
        void Update(Team team);
        void Delete(Team team);
        void CheckIfTeamExists(Team team);
        IEnumerable<Team> GetAll();
    }
}