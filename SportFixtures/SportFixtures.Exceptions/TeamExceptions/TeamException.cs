using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SportFixtures.Exceptions.TeamExceptions
{
    public class TeamException : Exception
    {
        public TeamException(string message) : base(message)
        {
        }
    }
}
