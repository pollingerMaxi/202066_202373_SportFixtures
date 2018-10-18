using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Logger
{
    public interface ILogger
    {
        void Info(object message);
        void Info(object message, Exception exception);
        void Debug(object message);
        void Debug(object message, Exception exception);
        void Error(object message);
        void Error(object message, Exception exception);
    }
}
