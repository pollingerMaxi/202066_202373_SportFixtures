using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.UserExceptions;
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
            var user = new User() { Name = "name", Username = "username", LastName = "lastname", Password = "hash", Email = "email@email.com" };
            var list = new List<User>();
            var mockRepo = new Mock<IRepository<User>>();
            mockRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => list.Add(user));
            IUserBusinessLogic userBL = new UserBusinessLogic(mockRepo.Object);
            userBL.AddUser(user);
            mockRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void AddUserWithUsernameTest()
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

        [TestMethod]
        [ExpectedException(typeof(InvalidUserNameException))]
        public void AddUserWithInvalidNameTest()
        {
            var user = new User() { Name = " ", Username = "username", LastName = "lastname", Password = "hash", Email = "email@email.com" };
            var list = new List<User>();
            var mockRepo = new Mock<IRepository<User>>();
            mockRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => list.Add(user));
            IUserBusinessLogic userBL = new UserBusinessLogic(mockRepo.Object);
            userBL.AddUser(user);
            mockRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserEmailException))]
        public void AddUserWithEmptyEmailTest()
        {
            var user = new User() { Name = "Name", Username = "username", LastName = "lastname", Password = "hash", Email = " " };
            var list = new List<User>();
            var mockRepo = new Mock<IRepository<User>>();
            mockRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => list.Add(user));
            IUserBusinessLogic userBL = new UserBusinessLogic(mockRepo.Object);
            userBL.AddUser(user);
            mockRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserEmailException))]
        public void AddUserWithInvalidEmailTest()
        {
            var user = new User() { Name = "Name", Username = "username", LastName = "lastname", Password = "hash", Email = "something" };
            var list = new List<User>();
            var mockRepo = new Mock<IRepository<User>>();
            mockRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => list.Add(user));
            IUserBusinessLogic userBL = new UserBusinessLogic(mockRepo.Object);
            userBL.AddUser(user);
            mockRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(UserException), AllowDerivedTypes = true)]
        public void AddUserWithInvalidFieldsTest()
        {
            var user = new User() { Name = "Name", Username = " ", LastName = "", Password = "hash", Email = "something.com" };
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
