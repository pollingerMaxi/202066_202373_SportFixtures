using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;
using SportFixtures.Data;

namespace SportFixtures.FixtureGenerator
{
    public interface IFixtureSelector
    {
        /// <summary>
        /// Generates encounters starting on the given date, for the given teams
        /// </summary>
        /// <param name="teams"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        ICollection<Encounter> GenerateFixture(IEnumerable<Team> teams, DateTime date);

        /// <summary>
        /// Returns a list with the algorithms names by encounter mode.
        /// </summary>
        /// <returns></returns>
        ICollection<string> GetAlgorithmNamesByEncounterMode(EncounterMode encounterMode);
    }
}
