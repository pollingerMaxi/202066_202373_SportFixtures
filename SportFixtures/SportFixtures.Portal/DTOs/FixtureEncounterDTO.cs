using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.DTOs
{
    public class FixtureEncounterDTO
    {
        public DateTime Date { get; set; }
        public int SportId { get; set; }
        public ICollection<EncountersTeamsDTO> Teams { get; set; }

        public FixtureEncounterDTO()
        {
            Teams = new List<EncountersTeamsDTO>();
        }
    }
}
