using System;
using SportFixtures.Data.Entities;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.EncounterExceptions;
using SportFixtures.FixtureGenerator;
using SportFixtures.FixtureGenerator.Implementations;
using System.Collections.Generic;
using System.Linq;
using SportFixtures.Data;

namespace SportFixtures.BusinessLogic.Implementations
{
    public class EncounterBusinessLogic : IEncounterBusinessLogic
    {
        private IRepository<Encounter> repository;
        private ISportBusinessLogic sportBL;

        public EncounterBusinessLogic(IRepository<Encounter> repository, ISportBusinessLogic sportBL)
        {
            this.repository = repository;
            this.sportBL = sportBL;
        }

        public void Add(Encounter encounter)
        {
            Validate(encounter);
            repository.Insert(encounter);
            repository.Save();
        }

        private void Validate(Encounter encounter)
        {
            ValidateResults(encounter);
            CheckSportEncounterModeAndTeamCount(encounter);
            if (encounter.Teams.Count < 2)
            {
                throw new EncounterTeamsCantBeNullException();
            }

            if (CheckDuplicatedTeams(encounter))
            {
                throw new EncounterSameTeamException();
            }

            if (TeamsHaveDifferentSport(encounter))
            {
                throw new EncounterTeamsDifferentSportException();
            }

            if (encounter.SportId != encounter.Teams.FirstOrDefault().SportId)
            {
                throw new EncounterSportDifferentFromTeamsSportException();
            }

            if (TeamsHaveEncountersOnTheSameDay(encounter))
            {
                throw new TeamAlreadyHasAnEncounterOnTheSameDayException();
            }
        }

        private void CheckSportEncounterModeAndTeamCount(Encounter encounter)
        {
            Sport sport = sportBL.GetById(encounter.SportId);
            if(sport.EncounterMode == EncounterMode.Double && encounter.Teams.Count() > 2)
            {
                throw new SportDoesNotSupportMultipleTeamsEncounters();
            }
            
        }

        private bool CheckDuplicatedTeams(Encounter encounter){
            return encounter.Teams.GroupBy(n => n.Id).Any(c => c.Count() > 1);
        }

        private bool TeamsHaveDifferentSport(Encounter encounter){
            var duplicates = encounter.Teams.GroupBy(s => s.SportId).ToList();
            return duplicates.Count() > 1; 
        }

        public void Update(Encounter encounter)
        {
            CheckIfExists(encounter.Id);
            Validate(encounter);
            repository.Update(encounter);
            repository.Save();
        }

        public void Delete(int id)
        {
            CheckIfExists(id);
            repository.Delete(id);
            repository.Save();
        }

        public void CheckIfExists(int encounterId)
        {
            GetById(encounterId);
        }

        public bool TeamsHaveEncountersOnTheSameDay(Encounter encounter)
        {
            foreach(Team team in encounter.Teams){
                if(repository.Get(null, null, "Teams").Any(e => ((e.Id != encounter.Id) && (e.Date.Date == encounter.Date.Date) && (e.Teams.Any(t => t.Id == team.Id))))){
                    return true;
                }
            }
            return false;
        }

        public void AddCommentToEncounter(Comment comment)
        {
            Encounter encounter = GetById(comment.EncounterId);
            encounter.Comments.Add(comment);
        }

        public Encounter GetById(int id)
        {
            return repository.Get(e => e.Id == id, null, "Teams").FirstOrDefault() ?? throw new EncounterDoesNotExistException();
        }

        public IEnumerable<Encounter> GetAll()
        {
            return repository.Get(null, null, "");
        }

        public IEnumerable<Encounter> GetAllEncountersOfSport(int sportId)
        {
            var encounters = repository.Get(e => e.SportId == sportId, null, "");
            if (encounters.Count() == 0)
            {
                throw new NoEncountersFoundForSportException();
            }
            return encounters;
        }

        public IEnumerable<Encounter> GetAllEncountersOfTeam(int teamId)
        {
            var encounters = repository.Get(e => (e.Teams.Any(t => t.Id == teamId)), null, "");
            if (encounters.Count() == 0)
            {
                throw new NoEncountersFoundForTeamException();
            }
            return encounters;
        }

        public IEnumerable<Encounter> GetAllEncountersOfTheDay(DateTime date)
        {
            var encounters = repository.Get(e => (e.Date.Date == date.Date), null, "");
            if (encounters.Count() == 0)
            {
                throw new NoEncountersFoundForDateException();
            }
            return encounters;
        }

        public bool TeamsHaveEncountersOnTheSameDay(ICollection<Encounter> encounters, Encounter encounter)
        {
            foreach(Team team in encounter.Teams){
                if(encounters.Any(e => ((e.Id != encounter.Id) && (e.Date.Date == encounter.Date.Date) && (e.Teams.Any(t => t.Id == team.Id))))){
                    return true;
                }
            }
            return false;
        }

        public ICollection<Encounter> GenerateFixture(DateTime date, int sportId, Algorithm fixtureGenerator)
        {
            ICollection<Team> teams = sportBL.GetById(sportId).Teams;
            IFixtureGenerator generator = null;
            if (fixtureGenerator == Algorithm.FreeForAll)
            {
                generator = new FreeForAll(this);
            }
            else if (fixtureGenerator == Algorithm.RoundRobin)
            {
                generator = new RoundRobin(this);
            }
            else
            {
                throw new FixtureGeneratorAlgorithmDoesNotExist();
            }
            return generator.GenerateFixture(teams, date);
        }

        private void ValidateResults(Encounter encounter)
        {
            if (encounter.Results.Count > 0)
            {
                foreach (Score result in encounter.Results)
                {
                    if (!encounter.Teams.Any(t => t.Id == result.TeamId))
                    {
                        throw new InvalidResultsForEncounterException();
                    }
                }
            }
            
        }
    }
}