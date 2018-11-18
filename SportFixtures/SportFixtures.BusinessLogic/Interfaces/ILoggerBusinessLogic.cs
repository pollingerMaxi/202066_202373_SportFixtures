using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface ILoggerBusinessLogic
    {
        ICollection<LogFile> GetLogsBetweenDates(DateTime from, DateTime to);
    }
}
