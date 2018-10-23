using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    public class InvalidResultsForEncounterException : EncounterException
    {
        public InvalidResultsForEncounterException() : base("Results teams are not of the encounter")
        {
        }
        public InvalidResultsForEncounterException(string message) : base(message)
        {
        }
    }
}