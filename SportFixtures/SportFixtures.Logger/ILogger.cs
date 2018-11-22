using System.IO;

namespace SportFixtures.Logger
{
    public interface ILogger
    {
        void LogWrite(string action, string logMessage, string user);
        void Log(string action, string logMessage, string user, TextWriter txtWriter);
    }
}