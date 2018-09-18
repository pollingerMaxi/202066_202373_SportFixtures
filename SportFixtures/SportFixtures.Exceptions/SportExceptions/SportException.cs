using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SportFixtures.Exceptions.SportExceptions
{
    public class SportException : Exception
    {
        public SportException(string message) : base(message)
        {
        }
    }
}
