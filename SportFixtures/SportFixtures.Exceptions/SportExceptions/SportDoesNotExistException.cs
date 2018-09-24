using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.SportExceptions
{
    [Serializable]
    public class SportDoesNotExistException : SportException
    {
        public SportDoesNotExistException() : base("Sport does not exist.")
        {
        }

        public SportDoesNotExistException(string message) : base(message)
        {
        }
    }
}