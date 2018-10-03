using System;
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
        public ICollection<UsersTeams> Favorites { get; set; }
        public Role Role { get; set; }
        public Guid? Token { get; set; }

        public User()
        {
            Favorites = new List<UsersTeams>();
        }
    }
}