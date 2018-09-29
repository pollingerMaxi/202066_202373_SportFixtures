using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Exceptions.EncounterExceptions
{
    public class EncounterTeamsCantBeNullException : EncounterException
    {
        public EncounterTeamsCantBeNullException() : base("An encounter should have two teams.")
        {
        }
        public EncounterTeamsCantBeNullException(string message) : base(message)
        {
        }
    }
}
