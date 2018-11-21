using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.FixtureSelectorExceptions
{
    [Serializable]
    public class ThereAreNoAlgorithmsException : FixtureSelectorException
    {
        public ThereAreNoAlgorithmsException() : base("There are no algorithms in folder.")
        {
        }

        public ThereAreNoAlgorithmsException(string message) : base(message)
        {
        }
    }
}