using System;
using SportFixtures.Data.Entities;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Repository;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using SportFixtures.Exceptions.SportExceptions;

namespace SportFixtures.BusinessLogic.Implementations
{
    public class SportBusinessLogic : ISportBusinessLogic
    {
        private IRepository<Sport> repository;

        public SportBusinessLogic(IRepository<Sport> repository)
        {
            this.repository = repository;
        }

        private bool UniqueName(string sportName)
        {
            return !repository.Get().Any(s => s.Name == sportName);
        }

        private void ValidateSport(Sport sport)
        {
            if (!UniqueName(sport.Name))
            {
                throw new DuplicatedSportNameException();
            }

            if (string.IsNullOrWhiteSpace(sport.Name))
            {
                throw new InvalidSportNameException();
            }
        }

        private void ValidateTeamInSport(Team team, Sport sport)
        {
            if (sport.Teams.Any(t => t.Name == team.Name))
            {
                throw new TeamAlreadyInSportException();
            }
        }

        public void Add(Sport sport)
        {
            ValidateSport(sport);
            repository.Insert(sport);
            repository.Save();

        }

        public void AddTeamToSport(Team team)
        {
            var sport = GetSportById(team.SportId);
            ValidateTeamInSport(team, sport);
            sport.Teams.Add(team);
            Update(sport);
        }

        private Sport GetSportById(int sportId)
        {
            var sport = repository.GetById(sportId);
            if (sport == null)
            {
                throw new SportDoesNotExistException();
            }
            return sport;
        }
        
        public void Update(Sport sport){
            CheckIfSportExists(sport);
            repository.Update(sport);
            repository.Save();
        }

        private void CheckIfSportExists(Sport sport)
        {
            if(repository.GetById(sport.Id) == null){
                throw new SportDoesNotExistException();
            }
        }
        
        public void Delete(Sport sport){
            CheckIfSportExists(sport);
            repository.Delete(sport);
            repository.Save();
        }

    }
}
