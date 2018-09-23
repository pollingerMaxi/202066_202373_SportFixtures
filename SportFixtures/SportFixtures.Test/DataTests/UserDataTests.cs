using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Test.DataTests
{
    [TestClass]
    public class UserDataTests
    {
        private Context context;
        private IRepository<User> repository;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "teamDB").Options;
            context = new Context(options);
            repository = new GenericRepository<User>(context);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var teams = repository.Get();
            context.RemoveRange(teams);
            context.SaveChanges();
        }

        [TestMethod]
        public void GetUsersWithNoUsersInRepositoryTest()
        {
            var mockRepo = new Mock<IRepository<User>>();
            var list = new List<User>();
            mockRepo.Setup(r => r.Get(null, null, "")).Returns(list);
            var users = (List<User>)mockRepo.Object.Get(null, null, "");
            mockRepo.Verify(x => x.Get(null, null, ""), Times.Once);
            Assert.IsTrue(users.Count == 0);
        }

        [TestMethod]
        public void GetUsersWithUserInRepositoryTest()
        {
            var mockRepo = new Mock<IRepository<User>>();
            var list = new List<User>() { new User() };
            mockRepo.Setup(r => r.Get(null, null, "")).Returns(list);
            var users = (List<User>)mockRepo.Object.Get(null, null, "");
            mockRepo.Verify(x => x.Get(null, null, ""), Times.Once);
            Assert.IsTrue(users.Count == 1);
        }

        [TestMethod]
        public void AddTeamToRepositoryTest()
        {
            var mockRepo = new Mock<IRepository<User>>();
            var list = new List<User>();
            var user = new User();
            mockRepo.Setup(r => r.Insert(It.IsAny<User>())).Callback<User>(x => list.Add(user));
            mockRepo.Object.Insert(user);
            mockRepo.Verify(x => x.Insert(user), Times.Once);
            Assert.IsTrue(list.Count == 1);
        }

        [TestMethod]
        public void UpdateUserInRepositoryTest()
        {
            var mockRepo = new Mock<IRepository<User>>();
            var user = new User() { Name = "InitialName" };
            var list = new List<User>() { user };
            mockRepo.Setup(r => r.Insert(user)).Callback<User>(x => list.Add(user));
            mockRepo.Object.Insert(user);
            user.Name = "UpdatedName";
            mockRepo.Object.Update(user);
            mockRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once);
            mockRepo.Verify(x => x.Update(user), Times.Once);
            Assert.IsTrue(list.Find(u => u.Name == "UpdatedName") == user);
        }
    }
}
