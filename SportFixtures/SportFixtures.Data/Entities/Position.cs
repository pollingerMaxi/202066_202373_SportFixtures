using System;
using System.Collections.Generic;
using System.Text;


namespace SportFixtures.Data.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public int sportId { get; set; }
        public Team Team { get; set; }
        public int points { get; set; }
    }
}