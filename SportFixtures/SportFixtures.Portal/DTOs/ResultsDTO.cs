using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.DTOs
{
    public class ResultsDTO
    {
        public ICollection<PositionInEncounter> Positions { get; set; }
        public int EncounterId { get; set; }
    }
}
