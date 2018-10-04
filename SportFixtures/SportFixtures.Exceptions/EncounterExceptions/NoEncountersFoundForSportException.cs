using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    [Serializable]
    public class NoEncountersFoundForSportException : EncounterException
    {
        public NoEncountersFoundForSportException() : base("No encounters found for the given sport.")
        {
        }

        public NoEncountersFoundForSportException(string message) : base(message)
        {
        }
    }
}