using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;
using SportFixtures.FixtureGenerator;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface IEncounterBusinessLogic
    {
        void Add(Encounter encounter);
        void Update(Encounter encounter);
        void Delete(int id);
        void CheckIfExists(int encounterId);
        void AddCommentToEncounter(Comment comment);
        Encounter GetById(int id);
        bool TeamsHaveEncountersOnTheSameDay(Encounter encounter);
        bool TeamsHaveEncountersOnTheSameDay(ICollection<Encounter> encounters, Encounter encounter);
        IEnumerable<Encounter> GetAll();
        IEnumerable<Encounter> GetAllEncountersOfSport(int sportId);
        IEnumerable<Encounter> GetAllEncountersOfTeam(int teamId);
        IEnumerable<Encounter> GetAllEncountersOfTheDay(DateTime date);
        ICollection<Encounter> GenerateFixture(DateTime date, int sportId, Algorithm fixtureGenerator);
    }
}