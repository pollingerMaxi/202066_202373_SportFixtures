using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface IEncounterBusinessLogic
    {
        void Add(Encounter encounter);
        void Update(Encounter encounter);
        void Delete(Encounter encounter);
        void CheckIfExists(int encounterId);
        void AddCommentToEncounter(Comment comment);
        IEnumerable<Encounter> GetAll();
    }
}