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
        public void GetAllTeamsWithFiltersTest()
        {
            Team nacional = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            Team peñarol = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            Team cerro = new Team() { Id = 3, Name = "Cerro", SportId = 1 };
            repository.Insert(nacional);
            repository.Insert(peñarol);
            repository.Insert(cerro);
            context.SaveChanges();
            var teams = repository.Get(null, q=>q.OrderBy(t => t.Name), "");
            Assert.IsTrue(teams.Count() == 3);
            Assert.IsTrue(teams.First<Team>().Equals(cerro));
        }

        [TestMethod]
        public void GetTeamWithNameFilterTest()
        {
            Team nacional = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            Team peñarol = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            Team cerro = new Team() { Id = 3, Name = "Cerro", SportId = 1 };
            repository.Insert(nacional);
            repository.Insert(peñarol);
            repository.Insert(cerro);
            context.SaveChanges();
            var teams = repository.Get(t=>t.Name == nacional.Name, q => q.OrderBy(t => t.Name), "");
            Assert.IsTrue(teams.Count() == 1);
            Assert.IsTrue(teams.First<Team>().Equals(nacional));
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
            var team = new Team { Name = "Nacional", SportId = 1 };
            repository.Insert(team);
            context.SaveChanges();
            Assert.IsTrue(repository.GetById(team.Id).Equals(team));
        }

        [TestMethod]
        public void GetTeamByIdNotEqualsToTeamTest()
        {
            var nacional = new Team { Name = "Nacional", SportId = 1 };
            var cerro = new Team { Name = "Cerro", SportId = 1 };
            repository.Insert(nacional);
            context.SaveChanges();
            Assert.IsFalse(repository.GetById(nacional.Id).Equals(cerro));
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
