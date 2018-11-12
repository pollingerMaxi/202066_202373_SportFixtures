using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;

namespace SportFixtures.Test.DataTests
{
    [TestClass]
    public class TeamDataTests
    {
        private Context context;
        private IRepository<Team> repository;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "teamDB").Options;
            context = new Context(options);
            repository = new GenericRepository<Team>(context);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var teams = repository.Get();
            context.RemoveRange(teams);
            context.SaveChanges();
        }

        [TestMethod]
        public void GetAllTeamsWithNoTeamsInRepositoryTest()
        {
            var teams = repository.Get();
            Assert.IsTrue(teams.Count() == 0);
        }

        [TestMethod]
        public void GetAllTeamsWithOneTeamInRepositoryTest()
        {
            repository.Insert(new Team());
            context.SaveChanges();
            var teams = repository.Get();
            Assert.IsTrue(teams.Count() == 1);
        }

        [TestMethod]
        public void AddTeamWithNameTest()
        {
            var team = new Team() { Name = "Test name" };
            repository.Insert(team);
            context.SaveChanges();
            Assert.IsTrue(repository.Get().First().Name == team.Name);
        }

        [TestMethod]
        public void AddTeamWithPhotoTest()
        {
            var team = new Team() { Photo = @"C:\photos\photo.png" };
            repository.Insert(team);
            context.SaveChanges();
            Assert.IsTrue(repository.Get().First().Photo == team.Photo);
        }

        [TestMethod]
        public void GetTeamByIdTest()
        {
            var team = new Team();
            repository.Insert(team);
            context.SaveChanges();
            Assert.IsTrue(repository.GetById(team.Id) == team);
        }

        [TestMethod]
        public void DeleteTeamByIdTest()
        {
            var team = new Team();
            repository.Insert(team);
            context.SaveChanges();
            repository.Delete(team.Id);
            context.SaveChanges();
            Assert.IsTrue(repository.Get().Count() == 0);
        }

        [TestMethod]
        public void DeleteTeamByObjectTest()
        {
            var team = new Team();
            repository.Insert(team);
            context.SaveChanges();
            repository.Delete(team);
            context.SaveChanges();
            Assert.IsTrue(repository.Get().Count() == 0);
        }

        [TestMethod]
        public void UpdateTeamTest()
        {
            var updatedName = "UpdatedName";
            var team = new Team() { Name = "InitialName" };
            repository.Insert(team);
            context.SaveChanges();
            team.Name = updatedName;
            repository.Update(team);
            context.SaveChanges();
            Assert.IsTrue(repository.GetById(team.Id).Name == updatedName);
        }

    }
}
