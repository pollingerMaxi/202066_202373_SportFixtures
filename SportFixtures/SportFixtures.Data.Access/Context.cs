﻿using Microsoft.EntityFrameworkCore;
using SportFixtures.Data.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SportFixtures.Data.Enums;

namespace SportFixtures.Data.Access
{
    public class Context : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UsersTeams> UsersTeams { get; set; }
        public DbSet<EncountersTeams> EncountersTeams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Encounter>().HasMany(c => c.Comments).WithOne().HasForeignKey(c => c.EncounterId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Encounter>().HasMany(c => c.Comments).WithOne().HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Sport>().HasMany(t => t.Teams).WithOne().HasForeignKey(t => t.SportId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UsersTeams>().HasKey(ut => new { ut.UserId, ut.TeamId });
            builder.Entity<UsersTeams>().HasOne<User>(ut => ut.User).WithMany(u => u.Favorites).HasForeignKey(ut => ut.UserId);
            builder.Entity<UsersTeams>().HasOne<Team>(ut => ut.Team).WithMany(u => u.FavoritedBy).HasForeignKey(ut => ut.TeamId);

            builder.Entity<EncountersTeams>().ToTable("EncountersTeams");
            builder.Entity<EncountersTeams>().HasKey(et => new { et.EncounterId, et.TeamId });
            builder.Entity<EncountersTeams>().HasOne<Encounter>(et => et.Encounter).WithMany(e => e.Teams).HasForeignKey(et => et.EncounterId);
            builder.Entity<EncountersTeams>().HasOne<Team>(et => et.Team).WithMany(t => t.Encounters).HasForeignKey(et => et.TeamId);
            
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admins Name",
                    LastName = "Admins LastName",
                    Username = "admin",
                    Email = "admin@admin.com",
                    Password = "admin",
                    Role = Role.Admin,
                });

            builder.Entity<User>().HasData(
                new User
                {
                    Id = 2,
                    Name = "Normal user",
                    LastName = "Users LastName",
                    Username = "user",
                    Email = "user@user.com",
                    Password = "user",
                    Role = Role.User,
                });

        }

        public Context() { }
        public Context(DbContextOptions<Context> options)
            : base(options) { }

    }
}
