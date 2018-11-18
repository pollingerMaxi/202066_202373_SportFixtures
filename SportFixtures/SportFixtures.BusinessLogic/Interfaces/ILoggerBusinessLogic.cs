using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface ILoggerBusinessLogic
    {
        ICollection<string> GetLogsBetweenDates(DateTime from, DateTime to);
    }
}
