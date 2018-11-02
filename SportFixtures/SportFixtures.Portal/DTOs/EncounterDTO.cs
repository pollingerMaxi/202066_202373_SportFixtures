using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.DTOs
{
    public class EncounterDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int SportId { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Score> Results { get; set; }

        public EncounterDTO()
        {
            Teams = new List<Team>();
            Comments = new List<Comment>();
            Results = new List<Score>();
        }
    }
}
