using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.CommentExceptions
{
    public class InvalidCommentTextException : CommentException
    {
        public InvalidCommentTextException() : base("Comment text cant be empty.")
        {
        }
        public InvalidCommentTextException(string message) : base(message)
        {
        }
    }
}
