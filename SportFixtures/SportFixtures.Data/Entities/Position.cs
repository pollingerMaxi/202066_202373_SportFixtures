using System;
using System.Collections.Generic;
using System.Text;


namespace SportFixtures.Data.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public int SportId { get; set; }
        public int Points { get; set; }

        public Position(){
            this.Points = 0;
        }
    }
}