using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    public class TeamAlreadyHasAnEncounterOnTheSameDayException : EncounterException
    {
        public TeamAlreadyHasAnEncounterOnTheSameDayException() : base("The team already has an encounter on the same day.")
        {
        }
        public TeamAlreadyHasAnEncounterOnTheSameDayException(string message) : base(message)
        {
        }
    }
}
