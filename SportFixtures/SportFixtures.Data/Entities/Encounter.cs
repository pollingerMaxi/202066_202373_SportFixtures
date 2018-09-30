using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Data.Entities
{
    public class Encounter
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int SportId { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Encounter(){
            Comments = new List<Comment>();
        }
    }
}
