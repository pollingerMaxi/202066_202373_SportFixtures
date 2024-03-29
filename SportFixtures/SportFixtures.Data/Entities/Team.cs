﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Data.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public int SportId { get; set; }
        public ICollection<UsersTeams> FavoritedBy { get; set; }
        public ICollection<EncountersTeams> Encounters { get; set; }

        public Team()
        {
            FavoritedBy = new List<UsersTeams>();
            Encounters = new List<EncountersTeams>();
        }

        public override bool Equals(object obj)
        {
            bool equals = false;

            if (obj != null && this.GetType() == obj.GetType())
            {
                Team team = (Team)obj;
                equals = team.Name.Equals(Name) && team.SportId == SportId;
            }

            return equals;
        }
    }
}
