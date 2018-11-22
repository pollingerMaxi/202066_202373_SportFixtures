using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    [Serializable]
    public class SportDoesNotSupportMultipleTeamsEncountersException : EncounterException
    {
        public SportDoesNotSupportMultipleTeamsEncountersException() : base("Sport does not support encounters with multiple teams.")
        {
        }

        public SportDoesNotSupportMultipleTeamsEncountersException(string message) : base(message)
        {
        }
    }
}