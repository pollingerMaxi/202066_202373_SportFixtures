using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.UserExceptions
{
    [Serializable]
    public class InvalidUserLastNameException : UserException
    {
        public InvalidUserLastNameException() : base("Lastname is not valid.")
        {
        }

        public InvalidUserLastNameException(string message) : base(message)
        {
        }
    }
}