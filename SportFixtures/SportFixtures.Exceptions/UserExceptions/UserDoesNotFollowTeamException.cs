using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.UserExceptions
{
    [Serializable]
    public class UserDoesNotFollowTeamException : UserException
    {
        public UserDoesNotFollowTeamException() : base("User does not follow the given team.")
        {
        }

        public UserDoesNotFollowTeamException(string message) : base(message)
        {
        }
    }
}