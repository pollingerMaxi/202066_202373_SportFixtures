using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    [Serializable]
    public class NoEncountersFoundForDateException : EncounterException
    {
        public NoEncountersFoundForDateException() : base("No encounters found for the given date.")
        {
        }

        public NoEncountersFoundForDateException(string message) : base(message)
        {
        }
    }
}