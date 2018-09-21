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

        public void ValidateSport(Sport sport)
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

        public void AddSport(Sport sport)
        {
            ValidateSport(sport);
            repository.Insert(sport);
            repository.Save();

        }

        public void AddTeamToSport(Team team, Sport sport)
        {
            if(!TeamIsInSport(team, sport)){
                sport.Teams.Add(team);
                repository.Update(sport);
                repository.Save();
            }
            
        }

        public bool TeamIsInSport(Team team, Sport sport){
            return sport.Teams.Any(t => t.Id == team.Id);
        }

        //public bool TeamIsInAnySport(Team team){}
    }
}
