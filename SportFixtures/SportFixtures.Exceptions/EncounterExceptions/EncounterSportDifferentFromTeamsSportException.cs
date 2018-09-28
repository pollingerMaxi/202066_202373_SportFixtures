using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    public class EncounterSportDifferentFromTeamsSportException : EncounterException
    {
        public EncounterSportDifferentFromTeamsSportException() : base("An encounter sport should be the sameone as the teams sport.")
        {
        }
        public EncounterSportDifferentFromTeamsSportException(string message) : base(message)
        {
        }
    }
}
