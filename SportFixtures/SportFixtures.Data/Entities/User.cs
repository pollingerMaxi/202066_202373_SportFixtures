using System.Collections.Generic;

namespace SportFixtures.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<Team> FollowedTeams { get; set; }
        public Role Role { get; set; }

        public User()
        {
            FollowedTeams = new List<Team>();
        }
    }
}