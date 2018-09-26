using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Test.BusinessLogicTests
{
    [TestClass]
    public class UserBusinessLogicTests
    {
        [TestMethod]
        public void AddUserOkTest()
        {
            var userName = "UserName";
            var list = new List<User>();
            var mockRepo = new Mock<IRepository<User>>();
            mockRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => list.Add(new User() { Name = userName }));
            IUserBusinessLogic userBL = new UserBusinessLogic(mockRepo.Object);
            userBL.AddUser(new User() { Name = userName });
            mockRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void AddUserWithUsernameTest()
        {
            var username = "UserNickname";
            var list = new List<User>();
            var mockRepo = new Mock<IRepository<User>>();
            mockRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => list.Add(new User() { Username = username }));
            IUserBusinessLogic userBL = new UserBusinessLogic(mockRepo.Object);
            userBL.AddUser(new User() { Username = username });
            mockRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void AddUserWithFullInfoOkTest()
        {
            var user = new User() { Name = "name", Username = "username", LastName = "lastname", Password = "hash", Email = "email@email.com" };
            var list = new List<User>();
            var mockRepo = new Mock<IRepository<User>>();
            mockRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => list.Add(user));
            IUserBusinessLogic userBL = new UserBusinessLogic(mockRepo.Object);
            userBL.AddUser(user);
            mockRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockRepo.Verify(x => x.Save(), Times.Once());
        }
    }
}
