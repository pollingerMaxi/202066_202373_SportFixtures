using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;
using SportFixtures.FixtureGenerator;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface IPositionBusinessLogic
    {
        void Add(Position position);
        void Update(Position position);
        void UpdatePositions(int sportId);
    }
}