using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.FixtureSelectorExceptions
{
    [Serializable]
    public class AlgorithmDoesNotExistException : FixtureSelectorException
    {
        public AlgorithmDoesNotExistException() : base("There are no algorithms with that name.")
        {
        }

        public AlgorithmDoesNotExistException(string message) : base(message)
        {
        }
    }
}