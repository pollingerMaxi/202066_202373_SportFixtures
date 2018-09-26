using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.UserExceptions
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {
        }
    }
}
