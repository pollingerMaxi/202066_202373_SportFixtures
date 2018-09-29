using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SportFixtures.Exceptions.CommentExceptions
{
    public class CommentException : Exception
    {
        public CommentException(string message) : base(message)
        {
        }
    }
}
