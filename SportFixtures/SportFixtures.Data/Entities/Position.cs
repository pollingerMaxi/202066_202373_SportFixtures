using System;
using System.Collections.Generic;
using System.Text;


namespace SportFixtures.Data.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public int points { get; set; }

        public Position(){
            this.points = 0;
        }
    }
}