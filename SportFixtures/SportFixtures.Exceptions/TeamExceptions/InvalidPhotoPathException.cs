using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.TeamExceptions
{
    public class InvalidPhotoPathException : TeamException
    {
        public InvalidPhotoPathException() : base("Path is invalid")
        {
        }
        public InvalidPhotoPathException(string message) : base(message)
        {
        }
    }
}
