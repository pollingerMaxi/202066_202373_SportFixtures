using System;
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
                string path = m_exePath + "\\" + DateTime.Today.ToString("dd/MM/yyyy") + ".txt";
                using (StreamWriter w = File.AppendText(path))
                {
                    Log(action, logMessage, user, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Log(string action, string logMessage, string user,TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  User:{0}", user);
                txtWriter.WriteLine("  Action:{0}", action);
                txtWriter.WriteLine("  Message:{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
