using System;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface ITeamBusinessLogic
    {
        void AddTeam(Team team);
        void UpdateTeam(Team team);
    }
}