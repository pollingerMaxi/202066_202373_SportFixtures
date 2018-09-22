using System;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface ITeamBusinessLogic
    {
        void AddTeam(Team team);
        bool TeamExistsById(int teamId);
        void UpdateSportIdOfTeam(Team team, Sport sport);
    }
}