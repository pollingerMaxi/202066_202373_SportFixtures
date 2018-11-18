using SportFixtures.BusinessLogic.Interfaces;
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
        public ICollection<string> GetLogsBetweenDates(DateTime from, DateTime to)
        {
            var m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(m_exePath + "\\logs\\");
            var files = Directory.GetFiles(m_exePath + "\\logs\\").Select(Path.GetFileName).ToArray();
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
