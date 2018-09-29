using System;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface IEncounterBusinessLogic
    {
        void Add(Encounter encounter);
        void Update(Encounter encounter);
        void Delete(Encounter encounter);
        void CheckTeamsEncountersDate(Encounter encounter);
    }
}