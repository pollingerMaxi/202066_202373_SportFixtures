using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    [Serializable]
    public class NotEnoughTeamsForEncounterException : EncounterException
    {
        public NotEnoughTeamsForEncounterException() : base("Not enough teams to create an encounter.")
        {
        }

        public NotEnoughTeamsForEncounterException(string message) : base(message)
        {
        }
    }
}