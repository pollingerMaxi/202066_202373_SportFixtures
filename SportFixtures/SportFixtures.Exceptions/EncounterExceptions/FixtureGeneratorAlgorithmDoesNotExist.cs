using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    public class FixtureGeneratorAlgorithmDoesNotExist : EncounterException
    {
        public FixtureGeneratorAlgorithmDoesNotExist() : base("Fixture generator algorithm does not exist.")
        {
        }
        public FixtureGeneratorAlgorithmDoesNotExist(string message) : base(message)
        {
        }
    }
}
