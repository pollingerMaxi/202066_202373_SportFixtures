using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.UserExceptions
{
    [Serializable]
    public class LoggedUserIsNotAdminException : UserException
    {
        public LoggedUserIsNotAdminException() : base("Logged user is not an admin.")
        {
        }

        public LoggedUserIsNotAdminException(string message) : base(message)
        {
        }
    }
}