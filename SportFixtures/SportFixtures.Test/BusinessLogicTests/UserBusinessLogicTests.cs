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
        private const dynamic NO_BUSINESS_LOGIC = null;

        [TestMethod]
        public void AddUserOkTest()
        {
            var user = new User() { Name = "name", Username = "username", LastName = "lastname", Password = "hash", Email = "email@email.com" };
            var list = new List<User>();
            var mockRepo = new Mock<IRepository<User>>();
            mockRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => list.Add(user));
            IUserBusinessLogic userBL = new UserBusinessLogic(mockRepo.Object, NO_BUSINESS_LOGIC);
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
            IUserBusinessLogic userBL = new UserBusinessLogic(mockRepo.Object, NO_BUSINESS_LOGIC);
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
            IUserBusinessLogic userBL = new UserBusinessLogic(mockRepo.Object, NO_BUSINESS_LOGIC);
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
            IUserBusinessLogic userBL = new UserBusinessLogic(mockRepo.Object, NO_BUSINESS_LOGIC);
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
            IUserBusinessLogic userBL = new UserBusinessLogic(mockRepo.Object, NO_BUSINESS_LOGIC);
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
            IUserBusinessLogic userBL = new UserBusinessLogic(mockRepo.Object, NO_BUSINESS_LOGIC);
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
            IUserBusinessLogic userBL = new UserBusinessLogic(mockRepo.Object, NO_BUSINESS_LOGIC);
            userBL.AddUser(user);
            mockRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void FollowTeamTest()
        {
            var user = new User() { Name = "Name", Username = "nick33", LastName = "surname", Password = "hash", Email = "a@a.com" };
            var team = new Team();
            var mockUserRepo = new Mock<IRepository<User>>();
            var mockTeamRepo = new Mock<IRepository<Team>>();
            var teamBL = new TeamBusinessLogic(mockTeamRepo.Object, NO_BUSINESS_LOGIC);
            var userBL = new UserBusinessLogic(mockUserRepo.Object, teamBL);
            mockTeamRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(team);
            mockUserRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(user);
            userBL.FollowTeam(user, team);
            mockTeamRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            mockUserRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            Assert.IsTrue(user.FollowedTeams.Contains(team));
        }

        [TestMethod]
        [ExpectedException(typeof(UserDoesNotExistException))]
        public void FollowTeamWithInvalidUserTest()
        {
            var user = new User() { Name = "Name", Username = "nick33", LastName = "surname", Password = "hash", Email = "a@a.com" };
            var team = new Team();
            var mockUserRepo = new Mock<IRepository<User>>();
            var mockTeamRepo = new Mock<IRepository<Team>>();
            var teamBL = new TeamBusinessLogic(mockTeamRepo.Object, NO_BUSINESS_LOGIC);
            var userBL = new UserBusinessLogic(mockUserRepo.Object, teamBL);
            mockTeamRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(team);
            userBL.FollowTeam(user, team);
        }
    }
}
