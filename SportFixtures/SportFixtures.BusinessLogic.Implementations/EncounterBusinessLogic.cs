using System;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Enums;
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
        private IRepository<EncountersTeams> encountersTeamsRepositry;
        private ISportBusinessLogic sportBL;

        public EncounterBusinessLogic(IRepository<Encounter> repository, ISportBusinessLogic sportBL)
        {
            this.repository = repository;
            this.sportBL = sportBL;
        }

        public void Add(Encounter encounter)
        {
            Validate(encounter);
            repository.Attach(encounter);
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

            if (encounter.SportId != encounter.Teams.FirstOrDefault().Team.SportId)
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
            if (sport.EncounterMode == EncounterMode.Double && encounter.Teams.Count() > 2)
            {
                throw new SportDoesNotSupportMultipleTeamsEncounters();
            }

        }

        private bool CheckDuplicatedTeams(Encounter encounter)
        {
            return encounter.Teams.GroupBy(n => n.TeamId).Any(c => c.Count() > 1);
        }

        private bool TeamsHaveDifferentSport(Encounter encounter)
        {
            var duplicates = encounter.Teams.GroupBy(s => s.Team.SportId).ToList();
            return duplicates.Count() > 1;
        }

        public void Update(Encounter encounter)
        {
            Validate(encounter);
            Encounter dbEncounter = GetById(encounter.Id);
            dbEncounter.Date = encounter.Date;
            dbEncounter.SportId = encounter.SportId;
            dbEncounter.Date = encounter.Date;
            dbEncounter.Comments = encounter.Comments;
            dbEncounter.Teams = encounter.Teams;
            repository.Update(dbEncounter);
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
            foreach (EncountersTeams team in encounter.Teams)
            {
                if (repository.Get(null, null, "Teams").Any(e => ((e.Id != encounter.Id) && (e.Date.Date == encounter.Date.Date) && (e.Teams.Any(t => t.TeamId == team.TeamId)))))
                {
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
            return repository.Get(null, null, "Teams.Team");
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
            var encounters = repository.Get(e => (e.Teams.Any(t => t.TeamId == teamId)), null, "Results");
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
            foreach (EncountersTeams team in encounter.Teams)
            {
                if (encounters.Any(e => ((e.Id != encounter.Id) && (e.Date.Date == encounter.Date.Date) && (e.Teams.Any(t => t.TeamId == team.TeamId)))))
                {
                    return true;
                }
            }
            return false;
        }

        private void ValidateResults(Encounter encounter)
        {
            if (encounter.Results.Count > 0)
            {
                foreach (PositionInEncounter result in encounter.Results)
                {
                    if (!encounter.Teams.Any(t => t.TeamId == result.TeamId))
                    {
                        throw new InvalidResultsForEncounterException();
                    }
                }
            }
        }

        public void AddResults(ICollection<PositionInEncounter> results, int encounterId)
        {
            Encounter encounter = GetById(encounterId);
            encounter.Results = results;
            ValidateResults(encounter);
            repository.Update(encounter);
            repository.Save();
        }

        public void AddMany(ICollection<Encounter> encounters){
            foreach(Encounter encounter in encounters){
                Validate(encounter);
                repository.Attach(encounter);
                repository.Insert(encounter);
            }
            repository.Save();
        }
    }
}