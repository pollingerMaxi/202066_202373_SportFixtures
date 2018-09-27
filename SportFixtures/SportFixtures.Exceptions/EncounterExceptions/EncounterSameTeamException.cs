using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    public class EncounterSameTeamException : EncounterException
    {
        public EncounterSameTeamException() : base("An encounter should have two diferent teams.")
        {
        }
        public EncounterSameTeamException(string message) : base(message)
        {
        }
    }
}
