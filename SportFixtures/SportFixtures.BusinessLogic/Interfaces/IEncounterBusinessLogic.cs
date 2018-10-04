using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;

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
        IEnumerable<Encounter> GetAll();
        IEnumerable<Encounter> GetAllEncountersOfSport(int sportId);
        IEnumerable<Encounter> GetAllEncountersOfTeam(int teamId);
        IEnumerable<Encounter> GetAllEncountersOfTheDay(DateTime date);
    }
}