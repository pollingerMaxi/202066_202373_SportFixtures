using System;
using SportFixtures.FixtureGenerator;

namespace SportFixtures.Portal.DTOs
{
    public class FixtureDTO
    {
        public DateTime Date { get; set; }
        public int SportId { get; set; }
        public Algorithm Algorithm { get; set; }
    }
}