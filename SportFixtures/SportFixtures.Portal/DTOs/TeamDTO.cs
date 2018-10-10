using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.DTOs
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public int SportId { get; set; }
        public ICollection<UsersTeams> FavoritedBy { get; set; }

        public TeamDTO()
        {
            FavoritedBy = new List<UsersTeams>();
        }
    }
}
