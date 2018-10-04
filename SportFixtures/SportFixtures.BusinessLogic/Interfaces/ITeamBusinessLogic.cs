using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface ITeamBusinessLogic
    {
        void Add(Team team);
        void Update(Team team);
        void Delete(int id);
        void CheckIfExists(int teamId);
        IEnumerable<Team> GetAll();
        Team GetById(int teamId);
    }
}