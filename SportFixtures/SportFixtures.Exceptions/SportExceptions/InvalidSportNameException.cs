using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.SportExceptions
{
    public class InvalidSportNameException : SportException
    {
        public InvalidSportNameException() : base("Sport name can't be empty.")
        {
        }
        public InvalidSportNameException(string message) : base(message)
        {
        }
    }
}
