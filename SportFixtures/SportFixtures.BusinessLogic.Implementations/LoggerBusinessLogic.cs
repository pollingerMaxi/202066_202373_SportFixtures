using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SportFixtures.BusinessLogic.Implementations
{
    public class LoggerBusinessLogic : ILoggerBusinessLogic
    {
        public ICollection<LogFile> GetLogsBetweenDates(DateTime from, DateTime to)
        {
            var m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(m_exePath + "\\logs\\");
            var files = Directory.GetFiles(m_exePath + "\\logs\\").Select(Path.GetFileName).ToArray();
            var fileNames = GetFilesList(from, to, m_exePath, files);
            var logs = new List<LogFile>();

            foreach (var file in fileNames)
            {
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        var logFile = new LogFile();
                        var splitted = line.Split('|');
                        logFile.LogEntry = splitted[0].Trim();
                        logFile.User = splitted[1].Trim();
                        logFile.Action = splitted[2].Trim();
                        logFile.Message = splitted[3].Trim();
                        logs.Add(logFile);
                    }
                }
            }
            return logs;
        }

        private static ICollection<string> GetFilesList(DateTime from, DateTime to, string m_exePath, string[] files)
        {
            var list = new List<string>();
            foreach (var file in files)
            {
                if (Int32.Parse(file.Remove(8)) >= Int32.Parse(from.ToString("yyyyMMdd")) &&
                    Int32.Parse(file.Remove(8)) <= Int32.Parse(to.ToString("yyyyMMdd")))
                {
                    list.Add(m_exePath + "\\logs\\" + file);
                }
            }
            return list;
        }
    }
}
