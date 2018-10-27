using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface IPositionTableCalculator
    {
        ICollection<Score> GeneratePositionTableForSport(int sportId);
    }
}