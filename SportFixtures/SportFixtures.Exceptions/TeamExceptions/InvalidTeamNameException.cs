using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.TeamExceptions
{
    public class InvalidTeamNameException : TeamException
    {
        public InvalidTeamNameException() : base("Team name can't be empty.")
        {
        }
        public InvalidTeamNameException(string message) : base(message)
        {
        }
    }
}
