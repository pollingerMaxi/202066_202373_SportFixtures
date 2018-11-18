using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Data.Entities
{
    public class LogFile
    {
        public string LogEntry { get; set; }
        public string User { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
    }
}
