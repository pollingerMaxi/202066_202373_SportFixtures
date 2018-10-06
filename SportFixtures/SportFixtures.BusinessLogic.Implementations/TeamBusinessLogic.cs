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
            repository.Insert(team);
            AddTeamToSport(team);
            repository.Save();
        }

        private void ValidateTeam(Team team)
        {
            if (string.IsNullOrWhiteSpace(team.Name))
            {
                throw new InvalidTeamNameException();
            }
            if (!ValidatePhotoPath(team.PhotoPath))
            {
                throw new InvalidPhotoPathException();
            }
        }

        private bool ValidatePhotoPath(String path)
        {
            bool pathIsValid = true;

            if (!string.IsNullOrWhiteSpace(path))
            {
                Regex r = new Regex(@"^(?:[\w]\:|\\)(\\[a-z_\-\s0-9\.]+)+\.(jpg|gif|jpeg|png)$");
                pathIsValid = r.IsMatch(path);
            }

            return pathIsValid;
        }

        private void AddTeamToSport(Team team)
        {
            sportBL.AddTeamToSport(team);
        }

        public void Update(Team team)
        {
            CheckIfExists(team.Id);
            ValidateTeam(team);
            repository.Update(team);
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