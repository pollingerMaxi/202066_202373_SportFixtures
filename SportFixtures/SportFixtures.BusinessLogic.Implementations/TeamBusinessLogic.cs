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

        public void Add(Team team)
        {
            ValidateTeam(team);
            AddTeamToSport(team);
            repository.Insert(team);
            repository.Save();
        }

        private void ValidateTeam(Team team)
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

        public void Update(Team team)
        {
            ValidateTeam(team);
            Team dbTeam = GetById(team.Id);
            dbTeam.Name = team.Name;
            dbTeam.Photo = team.Photo;
            repository.Update(dbTeam);
            repository.Save();
        }

        public void CheckIfExists(int teamId)
        {
            if (repository.GetById(teamId) == null)
            {
                throw new TeamDoesNotExistsException();
            }
        }

        public void Delete(int id)
        {
            CheckIfExists(id);
            repository.Delete(id);
            repository.Save();
        }

        public IEnumerable<Team> GetAll()
        {
            return repository.Get(null, null, "");
        }

        public Team GetById(int teamId)
        {
            return repository.GetById(teamId) ?? throw new TeamDoesNotExistsException();
        }
    }
}
