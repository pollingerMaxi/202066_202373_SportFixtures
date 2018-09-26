using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.SportExceptions
{
    [Serializable]
    public class TeamAlreadyInSportException : SportException
    {
        public TeamAlreadyInSportException() : base("Team is already in this sport.")
        {
        }

        public TeamAlreadyInSportException(string message) : base(message)
        {
        }
    }
}