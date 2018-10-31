using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Data.Entities
{
    public class PositionInEncounter
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int Position { get; set; }
    }
}
