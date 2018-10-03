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
        void CheckIfExists(int teamId);
        IEnumerable<Team> GetAll();
        Team GetById(int teamId);
    }
}