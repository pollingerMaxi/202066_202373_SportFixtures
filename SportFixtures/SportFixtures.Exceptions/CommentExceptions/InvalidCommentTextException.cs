using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.CommentExceptions
{
    public class InvalidCommentTextException : CommentException
    {
        public InvalidCommentTextException() : base("Encounter does not exists.")
        {
        }
        public InvalidCommentTextException(string message) : base(message)
        {
        }
    }
}
