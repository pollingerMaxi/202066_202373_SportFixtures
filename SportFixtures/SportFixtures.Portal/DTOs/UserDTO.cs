using SportFixtures.Data;
using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public ICollection<UsersTeams> Favorites { get; set; }
        public Role Role { get; set; }
        public Guid? Token { get; set; }

        public UserDTO()
        {
            Favorites = new List<UsersTeams>();
        }
    }
}
