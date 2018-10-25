using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SportFixtures.Exceptions.PositionExceptions
{
    public class PositionException : Exception
    {
        public PositionException(string message) : base(message)
        {
        }
    }
}
