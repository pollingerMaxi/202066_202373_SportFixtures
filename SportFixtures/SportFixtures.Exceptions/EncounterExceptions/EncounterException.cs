using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    public class EncounterException : Exception
    {
        public EncounterException(string message) : base(message)
        {
        }
    }
}
