using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;

namespace SportFixtures.FixtureGenerator
{
    public interface IFixtureGenerator
    {
        ICollection<Encounter> GenerateFixture(ICollection<Team> teams, DateTime date, int sportId);
    }
}
