using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    public class EncounterDoesNotExistException : EncounterException
    {
        public EncounterDoesNotExistException() : base("Encounter does not exist.")
        {
        }
        public EncounterDoesNotExistException(string message) : base(message)
        {
        }
    }
}
