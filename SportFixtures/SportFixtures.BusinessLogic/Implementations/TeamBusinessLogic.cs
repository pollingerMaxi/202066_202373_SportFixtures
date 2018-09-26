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
            CheckIfTeamExists(team);
            ValidateTeam(team);
            repository.Update(team);
            repository.Save();
        }

        private int GetTeamSportId(Team team)
        {
            return repository.GetById(team.Id).SportId;
        }

        public void CheckIfTeamExists(Team team)
        {
            if (repository.GetById(team.Id) == null)
            {
                throw new TeamDoesNotExistsException();
            }
        }

        public void Delete(Team team)
        {
            CheckIfTeamExists(team);
            repository.Delete(team);
            repository.Save();
        }
    }
}
