using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.SportExceptions
{
    public class DuplicatedSportNameException : SportException
    {
        public DuplicatedSportNameException() : base("Sport name already taken.")
        {
        }
        public DuplicatedSportNameException(string message) : base(message)
        {
        }
    }
}
