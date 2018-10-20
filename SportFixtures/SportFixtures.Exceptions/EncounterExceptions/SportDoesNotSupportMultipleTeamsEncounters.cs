using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    [Serializable]
    public class SportDoesNotSupportMultipleTeamsEncounters : EncounterException
    {
        public SportDoesNotSupportMultipleTeamsEncounters() : base("Sport does not support encounters with multiple teams.")
        {
        }

        public SportDoesNotSupportMultipleTeamsEncounters(string message) : base(message)
        {
        }
    }
}