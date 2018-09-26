using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.UserExceptions
{
    [Serializable]
    public class InvalidUserEmailException : UserException
    {
        public InvalidUserEmailException() : base("Email is not valid.")
        {
        }

        public InvalidUserEmailException(string message) : base(message)
        {
        }
    }
}