using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;

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
        ICollection<Encounter> GenerateFixture(IEnumerable<Team> teams, DateTime date);

        /// <summary>
        /// Returns a list with the algorithms obtained from the DLLs placed in the folders.
        /// </summary>
        /// <returns></returns>
        string[] GetFixtureAlgorithms();


        /// <summary>
        /// Creates an instance of the given algorithm, loaded from the DLLs folders.
        /// </summary>
        /// <param name="algorithmId"></param>
        void SetFixtureGenerator(int algorithmId);
    }
}
