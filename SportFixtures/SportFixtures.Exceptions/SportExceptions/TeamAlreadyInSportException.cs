using System;
using System.Runtime.Serialization;

namespace SportFixtures.Exceptions.SportExceptions
{
    [Serializable]
    public class TeamAlreadyInSportException : Exception
    {
        public TeamAlreadyInSportException()
        {
        }

        public TeamAlreadyInSportException(string message) : base(message)
        {
        }

        public TeamAlreadyInSportException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TeamAlreadyInSportException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}