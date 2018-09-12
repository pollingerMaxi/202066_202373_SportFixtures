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
        private UnitOfWork unitOfWork;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "teamDB").Options;
            context = new Context(options);
            unitOfWork = new UnitOfWork(context);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var teams = unitOfWork.TeamRepository.Get();
            context.RemoveRange(teams);
            context.SaveChanges();
        }

        [TestMethod]
        public void GetAllTeamsWithNoTeamsInRepository()
        {
            var teams = unitOfWork.TeamRepository.Get();
            Assert.IsTrue(teams.Count() == 0);
        }

        [TestMethod]
        public void GetAllTeamsWithOneTeamInRepository()
        {
            unitOfWork.TeamRepository.Insert(new Team());
            context.SaveChanges();
            var teams = unitOfWork.TeamRepository.Get();
            Assert.IsTrue(teams.Count() == 1);
        }

        [TestMethod]
        public void AddTeamWithName()
        {
            var team = new Team() { Name = "Test name" };
            unitOfWork.TeamRepository.Insert(team);
            context.SaveChanges();
            Assert.IsTrue(unitOfWork.TeamRepository.Get().First().Name == team.Name);
        }

        [TestMethod]
        public void AddTeamWithPhotoPath()
        {
            var team = new Team() { PhotoPath = @"C:\photos\photo.png"};
            unitOfWork.TeamRepository.Insert(team);
            context.SaveChanges();
            Assert.IsTrue(unitOfWork.TeamRepository.Get().First().PhotoPath == team.PhotoPath);
        }


    }
}
