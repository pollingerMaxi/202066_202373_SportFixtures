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
            AddTeamToSport(team);
            repository.Save();
        }

        public void ValidateTeam(Team team)
        {
            if (string.IsNullOrWhiteSpace(team.Name))
            {
                throw new InvalidTeamNameException();
            }
        }

        private void AddTeamToSport(Team team)
        {
            sportBL.AddTeamToSport(team);
        }
    }
}
