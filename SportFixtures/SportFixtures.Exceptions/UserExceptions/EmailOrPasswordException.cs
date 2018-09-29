using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.UserExceptions
{
    [Serializable]
    public class EmailOrPasswordException : UserException
    {
        public EmailOrPasswordException() : base("Email or password are incorrect.")
        {
        }

        public EmailOrPasswordException(string message) : base(message)
        {
        }
    }
}