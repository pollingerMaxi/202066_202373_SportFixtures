using System;
using System.Collections.Generic;
using SportFixtures.Data;
using SportFixtures.Data.Entities;
using SportFixtures.Data;

namespace SportFixtures.FixtureGenerator
{
    public interface IFixtureGenerator
    {
        /// <summary>
        /// Generates encounters starting on the given date, for the given teams
        /// </summary>
        /// <param name="teams"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        EncounterMode encounterMode {get; set;}
        ICollection<Encounter> GenerateFixture(IEnumerable<Team> teams, DateTime date);
    }
}
