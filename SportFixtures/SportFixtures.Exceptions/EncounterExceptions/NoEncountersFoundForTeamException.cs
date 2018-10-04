using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    [Serializable]
    public class NoEncountersFoundForTeamException : EncounterException
    {
        public NoEncountersFoundForTeamException() : base("No encounters found for the given team.")
        {
        }

        public NoEncountersFoundForTeamException(string message) : base(message)
        {
        }
    }
}