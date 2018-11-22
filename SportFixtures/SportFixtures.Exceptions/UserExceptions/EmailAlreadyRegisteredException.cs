using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.UserExceptions
{
    [Serializable]
    public class EmailAlreadyRegisteredException : UserException
    {
        public EmailAlreadyRegisteredException() : base("Email is already registered.")
        {
        }

        public EmailAlreadyRegisteredException(string message) : base(message)
        {
        }
    }
}