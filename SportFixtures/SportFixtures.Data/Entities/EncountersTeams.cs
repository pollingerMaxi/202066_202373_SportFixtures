using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Data.Entities
{
    public class EncountersTeams
    {
        public int EncounterId { get; set; }
        public Encounter Encounter { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
