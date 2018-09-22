using System;
using SportFixtures.Data.Entities;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Repository;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using SportFixtures.Exceptions.TeamExceptions;

namespace SportFixtures.BusinessLogic.Implementations
{
    public class TeamBusinessLogic : ITeamBusinessLogic
    {
        private IRepository<Team> repository;
        private ISportBusinessLogic sportBL;
        public TeamBusinessLogic(IRepository<Team> repository, ISportBusinessLogic sportBl)
        {
            this.repository = repository;
            this.sportBL = sportBl;
        }

        public void AddTeam(Team team)
        {
            ValidateTeam(team);
            repository.Insert(team);
            repository.Save();
        }
        public bool TeamExistsById(int teamId)
        {
            return repository.Get().Any(t => t.Id == teamId);
        }
       // private bool UniqueName(string teamName)
       // {
       //     return !repository.Get().Any(t => t.Name == teamName);
       // }
        public void ValidateTeam(Team team)
        {
            if (string.IsNullOrWhiteSpace(team.Name))
            {
                throw new InvalidTeamNameException();
            }
        }
        public void AddTeamToSport(Team team, Sport sport)
        {
            sportBL.AddTeamToSport(team, sport);
            this.UpdateSportIdOfTeam(team, sport);
        }

        private void UpdateSportIdOfTeam(Team team, Sport sport)
        {
            repository.Update(team);
        }
    }
}
