using System;
using SportFixtures.Data.Entities;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.EncounterExceptions;
using SportFixtures.FixtureGenerator;
using SportFixtures.FixtureGenerator.Implementations;
using System.Collections.Generic;
using System.Linq;

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
            if (encounter.Teams.Count < 2)
            {
                throw new EncounterTeamsCantBeNullException();
            }

            if (CheckDuplicatedTeams(encounter))
            {
                throw new EncounterSameTeamException();
            }

            if (!TeamsAreOnTheSameSport(encounter))
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

        private bool CheckDuplicatedTeams(Encounter encounter){
            return encounter.Teams.GroupBy(n => n.Id).Any(c => c.Count() > 1);
        }

        private bool TeamsAreOnTheSameSport(Encounter encounter){
            return encounter.Teams.GroupBy(n => n.SportId).Any(c => c.Count() == 1);
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
            if (repository.GetById(encounterId) == null)
            {
                throw new EncounterDoesNotExistException();
            }
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
            return repository.GetById(id) ?? throw new EncounterDoesNotExistException();
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
    }
}