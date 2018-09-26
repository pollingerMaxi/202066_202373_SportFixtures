using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.UserExceptions
{
    [Serializable]
    public class UserDoesNotExistException : UserException
    {
        public UserDoesNotExistException() : base("User does not exist.")
        {
        }

        public UserDoesNotExistException(string message) : base(message)
        {
        }
    }
}