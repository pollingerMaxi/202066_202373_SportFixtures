using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    public class FixtureGeneratorAlgorithmDoesNotExistException : EncounterException
    {
        public FixtureGeneratorAlgorithmDoesNotExistException() : base("Fixture generator algorithm does not exist.")
        {
        }
        public FixtureGeneratorAlgorithmDoesNotExistException(string message) : base(message)
        {
        }
    }
}
