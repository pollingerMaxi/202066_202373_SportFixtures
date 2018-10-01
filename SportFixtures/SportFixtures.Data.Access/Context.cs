using Microsoft.EntityFrameworkCore;
using SportFixtures.Data.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Data.Access
{
    public class Context : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=SportFixturesTest;Trusted_Connection=True;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Encounter>().HasMany(c => c.Comments).WithOne().HasForeignKey(c => c.EncounterId);
        }

        public Context() { }
        public Context(DbContextOptions<Context> options)
            : base(options) { }

    }
}
