using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Data.Entities
{
    public class UsersTeams
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
