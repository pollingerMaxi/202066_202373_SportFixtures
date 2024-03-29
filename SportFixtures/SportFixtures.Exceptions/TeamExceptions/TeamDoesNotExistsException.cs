using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.TeamExceptions
{
    public class TeamDoesNotExistsException : TeamException
    {
        public TeamDoesNotExistsException() : base("Team does not exist.")
        {
        }
        public TeamDoesNotExistsException(string message) : base(message)
        {
        }
    }
}
