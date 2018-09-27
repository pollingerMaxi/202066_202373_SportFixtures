using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.UserExceptions
{
    [Serializable]
    public class UsernameAlreadyInUseException : UserException
    {
        public UsernameAlreadyInUseException() : base("Username is already in use.")
        {
        }

        public UsernameAlreadyInUseException(string message) : base(message)
        {
        }
    }
}