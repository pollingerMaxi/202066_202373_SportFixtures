using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.UserExceptions
{
    [Serializable]
    public class InvalidUserUsernameException : UserException
    {
        public InvalidUserUsernameException() : base("Username is not valid.")
        {
        }

        public InvalidUserUsernameException(string message) : base(message)
        {
        }
    }
}