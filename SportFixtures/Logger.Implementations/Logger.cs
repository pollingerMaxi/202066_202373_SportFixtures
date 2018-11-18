using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace SportFixtures.Logger.Implementations
{
    public class Logger : ILogger
    {
        private string m_exePath = string.Empty;

        public void LogWrite(string action, string logMessage, string user)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                DirectoryInfo di = Directory.CreateDirectory(m_exePath + "\\logs\\");
                string path = m_exePath + "\\logs\\" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
                using (StreamWriter w = File.AppendText(path))
                {
                    Log(action, logMessage, user, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Log(string action, string logMessage, string user, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry: ");
                txtWriter.Write("{0}", DateTime.Now.ToString("dd-MM-yyyy'T'HH:mm:ss.fffK", CultureInfo.InvariantCulture));
                txtWriter.Write("  |  User: {0}", user);
                txtWriter.Write("  |  Action: {0}", action);
                txtWriter.Write("  |  Message: {0}", logMessage);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
