using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    public class EncounterDoesNotExistsException : EncounterException
    {
        public EncounterDoesNotExistsException() : base("Encounter does not exists.")
        {
        }
        public EncounterDoesNotExistsException(string message) : base(message)
        {
        }
    }
}
