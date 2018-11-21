using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SportFixtures.Exceptions.FixtureSelectorExceptions
{
    public class FixtureSelectorException : Exception
    {
        public FixtureSelectorException(string message) : base(message)
        {
        }
    }
}
