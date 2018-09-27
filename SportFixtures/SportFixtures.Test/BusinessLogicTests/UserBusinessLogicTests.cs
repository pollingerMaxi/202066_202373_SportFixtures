﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        private User userWithAllData;
        private IUserBusinessLogic userBLWithoutTeamBL;
        private Mock<IRepository<User>> mockUserRepo;
        private Mock<IRepository<Team>> mockTeamRepo;
        private List<User> userList;

        [TestInitialize]
        public void TestInitialize()
        {
            userWithAllData = new User() { Name = "name", Username = "username", LastName = "lastname", Password = "hash", Email = "email@email.com" };
            mockUserRepo = new Mock<IRepository<User>>();
            mockTeamRepo = new Mock<IRepository<Team>>();
            userList = new List<User>();
            userBLWithoutTeamBL = new UserBusinessLogic(mockUserRepo.Object, NO_BUSINESS_LOGIC);
            mockUserRepo.Setup(r => r.Get(null, null, "")).Returns(userList);
        }

        [TestMethod]
        public void AddUserOkTest()
        {
            mockUserRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => userList.Add(userWithAllData));
            userBLWithoutTeamBL.AddUser(userWithAllData);
            mockUserRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockUserRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void AddUserWithUsernameTest()
        {
            mockUserRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => userList.Add(userWithAllData));
            userBLWithoutTeamBL.AddUser(userWithAllData);
            mockUserRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockUserRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void AddUserWithFullInfoOkTest()
        {
            mockUserRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => userList.Add(userWithAllData));
            userBLWithoutTeamBL.AddUser(userWithAllData);
            mockUserRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockUserRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserNameException))]
        public void AddUserWithInvalidNameTest()
        {
            var user = new User() { Name = " ", Username = "username", LastName = "lastname", Password = "hash", Email = "email@email.com" };
            mockUserRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => userList.Add(user));
            userBLWithoutTeamBL.AddUser(user);
            mockUserRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockUserRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserEmailException))]
        public void AddUserWithEmptyEmailTest()
        {
            var user = new User() { Name = "Name", Username = "username", LastName = "lastname", Password = "hash", Email = " " };
            mockUserRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => userList.Add(user));
            userBLWithoutTeamBL.AddUser(user);
            mockUserRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockUserRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserEmailException))]
        public void AddUserWithInvalidEmailTest()
        {
            var user = new User() { Name = "Name", Username = "username", LastName = "lastname", Password = "hash", Email = "something" };
            mockUserRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => userList.Add(user));
            userBLWithoutTeamBL.AddUser(user);
            mockUserRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockUserRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(UserException), AllowDerivedTypes = true)]
        public void AddUserWithInvalidFieldsTest()
        {
            var user = new User() { Name = "Name", Username = " ", LastName = "", Password = "hash", Email = "something.com" };
            mockUserRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => userList.Add(user));
            userBLWithoutTeamBL.AddUser(user);
            mockUserRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockUserRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserLastNameException))]
        public void AddUserWithInvalidLastnameTest()
        {
            var user = new User() { Name = "Name", Username = "username", LastName = " ", Password = "hash", Email = "something@domain.com" };
            mockUserRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => userList.Add(user));
            userBLWithoutTeamBL.AddUser(user);
            mockUserRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockUserRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void FollowTeamTest()
        {
            var team = new Team();
            var teamBL = new TeamBusinessLogic(mockTeamRepo.Object, NO_BUSINESS_LOGIC);
            var userBL = new UserBusinessLogic(mockUserRepo.Object, teamBL);
            mockTeamRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(team);
            mockUserRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(userWithAllData);
            userBL.FollowTeam(userWithAllData, team);
            mockTeamRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            mockUserRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            Assert.IsTrue(userWithAllData.FollowedTeams.Contains(team));
        }

        [TestMethod]
        [ExpectedException(typeof(UserDoesNotExistException))]
        public void FollowTeamWithInvalidUserTest()
        {
            var user = new User() { Name = "Name", Username = "nick33", LastName = "surname", Password = "hash", Email = "a@a.com" };
            var team = new Team();
            var teamBL = new TeamBusinessLogic(mockTeamRepo.Object, NO_BUSINESS_LOGIC);
            var userBL = new UserBusinessLogic(mockUserRepo.Object, teamBL);
            mockTeamRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(team);
            userBL.FollowTeam(user, team);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            mockUserRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(userWithAllData);
            userBLWithoutTeamBL.Update(userWithAllData);
            mockUserRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            mockUserRepo.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(UserDoesNotExistException))]
        public void UpdateUserWithInvalidUserTest()
        {
            userBLWithoutTeamBL.Update(userWithAllData);
            mockUserRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void UsernameIsNotDuplicatedTest()
        {
            mockUserRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => userList.Add(userWithAllData));
            userBLWithoutTeamBL.AddUser(userWithAllData);
            mockUserRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(UsernameAlreadyInUseException))]
        public void UsernameIsDuplicatedTest()
        {
            mockUserRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => userList.Add(userWithAllData));
            userBLWithoutTeamBL.AddUser(userWithAllData);
            userBLWithoutTeamBL.AddUser(userWithAllData);
            mockUserRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Exactly(2));
        }
    }
}
