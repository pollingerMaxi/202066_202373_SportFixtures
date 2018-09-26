using System;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface ITeamBusinessLogic
    {
        void AddTeam(Team team);
        void Update(Team team);
        void Delete(Team team);
    }
}