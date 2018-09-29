using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    public class EncounterTeamsDifferentSportException : EncounterException
    {
        public EncounterTeamsDifferentSportException() : base("An encounter cant have teams from different sports.")
        {
        }
        public EncounterTeamsDifferentSportException(string message) : base(message)
        {
        }
    }
}
