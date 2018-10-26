using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface IPositionTableCalculator
    {
        ICollection<Position> GeneratePositionTable(int sportId);
    }
}