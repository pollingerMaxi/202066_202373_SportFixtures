using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.UserExceptions
{
    [Serializable]
    public class UserIsNotLoggedInException : UserException
    {
        public UserIsNotLoggedInException() : base("User is not currently logged in.")
        {
        }

        public UserIsNotLoggedInException(string message) : base(message)
        {
        }
    }
}