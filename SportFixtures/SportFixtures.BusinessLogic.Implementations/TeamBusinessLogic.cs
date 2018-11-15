using System;
using SportFixtures.Data.Entities;
using SportFixtures.Data.DTOs;
using SportFixtures.Data.Enums;
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

        public IEnumerable<Team> GetAll(TeamFilterDTO filter = null)
        {
            if(filter == null){
                return repository.Get(null, null, "");
            }

            if(filter.Name != null){
                return repository.Get(null, (q => q.OrderBy(t => t.Name)), "");
            }
            else{
                return filter.Order == Order.Descending ? repository.Get(t => t.Name == filter.Name, (q => q.OrderByDescending(t => t.Name)), "") :
                                repository.Get(t => t.Name == filter.Name, (q => q.OrderBy(t => t.Name)), "");
            }
        }

        public Team GetById(int teamId)
        {
            return repository.GetById(teamId) ?? throw new TeamDoesNotExistsException();
        }
    }
}
