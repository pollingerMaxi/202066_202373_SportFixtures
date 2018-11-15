using System.IO;

namespace SportFixtures.Logger
{
    public interface ILogger
    {
        void LogWrite(string logMessage, string user);
        void Log(string logMessage, string user,TextWriter txtWriter);
    }
}