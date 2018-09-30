using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.UserExceptions
{
    [Serializable]
    public class UserIsNotAdminException : UserException
    {
        public UserIsNotAdminException() : base("User's role is not admin.")
        {
        }

        public UserIsNotAdminException(string message) : base(message)
        {
        }
    }
}