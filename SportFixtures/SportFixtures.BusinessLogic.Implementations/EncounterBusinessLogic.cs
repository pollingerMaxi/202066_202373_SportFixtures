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
            if (encounter.Team1 == null || encounter.Team2 == null)
            {
                throw new EncounterTeamsCantBeNullException();
            }

            if (encounter.Team1 == encounter.Team2)
            {
                throw new EncounterSameTeamException();
            }

            if (encounter.Team1.SportId != encounter.Team2.SportId)
            {
                throw new EncounterTeamsDifferentSportException();
            }

            if (encounter.SportId != encounter.Team1.SportId)
            {
                throw new EncounterSportDifferentFromTeamsSportException();
            }

            if (TeamsHaveEncountersOnTheSameDay(encounter))
            {
                throw new TeamAlreadyHasAnEncounterOnTheSameDayException();
            }
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
            return (repository.Get(null, null, "Team1,Team2").Any(e => ((e.Id != encounter.Id) && (e.Date.Date == encounter.Date.Date) && (e.Team1.Equals(encounter.Team1) || e.Team2.Equals(encounter.Team1))))
                || repository.Get(null, null, "Team1,Team2").Any(e => ((e.Id != encounter.Id) && (e.Date.Date == encounter.Date.Date) && (e.Team1.Equals(encounter.Team2) || e.Team2.Equals(encounter.Team2)))));
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
            var encounters = repository.Get(e => ((e.Team1.Id == teamId) || (e.Team2.Id == teamId)), null, "");
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
            return (encounters.Any(e => ((e.Date.Date == encounter.Date.Date) && (e.Team1.Equals(encounter.Team1) || e.Team2.Equals(encounter.Team1))))
                || encounters.Any(e => ((e.Date.Date == encounter.Date.Date) && (e.Team1.Equals(encounter.Team2) || e.Team2.Equals(encounter.Team2)))));
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