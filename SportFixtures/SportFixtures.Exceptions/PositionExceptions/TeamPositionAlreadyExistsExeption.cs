using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.PositionExceptions
{
    public class TeamPositionAlreadyExistsExeption : PositionException
    {
        public TeamPositionAlreadyExistsExeption() : base("Team position is already registered")
        {
        }
        public TeamPositionAlreadyExistsExeption(string message) : base(message)
        {
        }
    }
}
