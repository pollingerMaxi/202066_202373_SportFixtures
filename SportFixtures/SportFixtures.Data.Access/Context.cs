using Microsoft.EntityFrameworkCore;
using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Data.Access
{
    public class Context : DbContext
    {
        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=SportFixturesTest;Trusted_Connection=True;Integrated Security=True");
        }
    }
}
