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
        public Team Home { get; set; }
        public Team Visitor { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public EncounterDTO()
        {
            Comments = new List<Comment>();
        }
    }
}
