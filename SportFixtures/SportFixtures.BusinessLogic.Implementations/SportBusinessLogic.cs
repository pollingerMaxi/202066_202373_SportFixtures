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

        private bool UniqueName(Sport sport)
        {
            return !repository.Get(null, null, "").Any(s => ((s.Name == sport.Name) && (s.Id != sport.Id)));
        }

        private void ValidateSport(Sport sport)
        {
            if (!UniqueName(sport))
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
            var sport = GetById(team.SportId);
            ValidateTeamInSport(team, sport);
        }

        public Sport GetById(int id)
        {
            var sport = repository.Get(s => s.Id == id, null, "Teams").FirstOrDefault();
            if (sport == null)
            {
                throw new SportDoesNotExistException();
            }
            return sport;
        }

        public void Update(Sport sport)
        {
            ValidateSport(sport);
            CheckIfSportExists(sport.Id);
            repository.Update(sport);
            repository.Save();
        }

        private void CheckIfSportExists(int id)
        {
            if (repository.GetById(id) == null)
            {
                throw new SportDoesNotExistException();
            }
        }

        public void Delete(int id)
        {
            CheckIfSportExists(id);
            repository.Delete(id);
            repository.Save();
        }

        public IEnumerable<Sport> GetAll()
        {
            return repository.Get(null, null, "");
        }

    }
}
