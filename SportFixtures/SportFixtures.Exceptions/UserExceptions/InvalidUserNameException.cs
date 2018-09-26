using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.UserExceptions
{
    [Serializable]
    public class InvalidUserNameException : UserException
    {
        public InvalidUserNameException() : base("Name is not valid.")
        {
        }

        public InvalidUserNameException(string message) : base(message)
        {
        }
    }
}