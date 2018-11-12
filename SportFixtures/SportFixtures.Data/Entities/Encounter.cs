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
        public ICollection<EncountersTeams> Teams { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PositionInEncounter> Results { get; set; }

        public Encounter(){
            Teams = new List<EncountersTeams>();
            Comments = new List<Comment>();
            Results = new List<PositionInEncounter>();
        }
    }
}
