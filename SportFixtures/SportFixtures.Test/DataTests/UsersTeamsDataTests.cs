using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SportFixtures.Test.DataTests
{
    [TestClass]
    public class UsersTeamsDataTests
    {
        private Context context;
        private IRepository<UsersTeams> repository;
        private User user;
        private Team team;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "usersTeamsDB").Options;
            context = new Context(options);
            repository = new GenericRepository<UsersTeams>(context);
            user = new User();
            team = new Team() { Name = "teamName" };
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var teams = repository.Get();
            context.RemoveRange(teams);
            context.SaveChanges();
        }

        [TestMethod]
        public void GetFavoritesWithNoFavoritesInRepositoryTest()
        {
            var mockRepo = new Mock<IRepository<UsersTeams>>();
            var list = new List<UsersTeams>();
            mockRepo.Setup(r => r.Get(null, null, "")).Returns(list);
            var usersteams = mockRepo.Object.Get(null, null, "").ToList();
            mockRepo.Verify(x => x.Get(null, null, ""), Times.Once);
            Assert.IsTrue(usersteams.Count == 0);
        }

        [TestMethod]
        public void GetFavoritesWithFavoritesInRepositoryTest()
        {
            var mockRepo = new Mock<IRepository<UsersTeams>>();
            var list = new List<UsersTeams>() { new UsersTeams() };
            mockRepo.Setup(r => r.Get(null, null, "")).Returns(list);
            var usersteams = mockRepo.Object.Get(null, null, "").ToList();
            mockRepo.Verify(x => x.Get(null, null, ""), Times.Once);
            Assert.IsTrue(usersteams.Count == 1);
        }

        [TestMethod]
        public void AddUserToRepositoryTest()
        {
            var mockRepo = new Mock<IRepository<UsersTeams>>();
            var list = new List<UsersTeams>();
            var userteam = new UsersTeams() { Team = team, User = user, TeamId = team.Id, UserId = user.Id };
            mockRepo.Setup(r => r.Insert(It.IsAny<UsersTeams>())).Callback<UsersTeams>(x => list.Add(userteam));
            mockRepo.Object.Insert(userteam);
            mockRepo.Verify(x => x.Insert(userteam), Times.Once);
            Assert.IsTrue(list.Count == 1);
        }

        [TestMethod]
        public void GetFavoritesOfTeamTest()
        {
            var mockRepo = new Mock<IRepository<UsersTeams>>();
            var list = new List<UsersTeams>() { new UsersTeams() { Team = team, User = user } };
            mockRepo.Setup(r => r.Get(It.IsAny<Expression<Func<UsersTeams, bool>>>(), null, "")).Returns(list);
            var usersteams = mockRepo.Object.Get(t => t.Team.Equals(team), null, "").ToList();
            var userteam = usersteams.First();
            mockRepo.Verify(x => x.Get(It.IsAny<Expression<Func<UsersTeams, bool>>>(), null, ""), Times.Once);
            Assert.IsTrue(usersteams.Count == 1);
            Assert.IsTrue(userteam.Team.Equals(team));
            Assert.IsTrue(userteam.TeamId.Equals(team.Id));
            Assert.IsTrue(userteam.User.Equals(user));
            Assert.IsTrue(userteam.UserId.Equals(user.Id));
        }


    }
}
