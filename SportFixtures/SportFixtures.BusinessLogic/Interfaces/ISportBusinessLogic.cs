using System;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface ISportBusinessLogic
    {
        void AddSport(Sport sport);
        void ValidateSport(Sport sport);
        void AddTeamToSport(Team team);
    }
}