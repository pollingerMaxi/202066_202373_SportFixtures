﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.UserExceptions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Collections;
using SportFixtures.Data.Enums;

namespace SportFixtures.Test.BusinessLogicTests
{
    [TestClass]
    public class UserBusinessLogicTests
    {
        private const dynamic NO_BUSINESS_LOGIC = null;
        private const dynamic NO_UT_REPOSITORY = null;
        private User userWithAllData;
        private User adminWithAllData;
        private IUserBusinessLogic userBLWithoutTeamBL;
        private Mock<IRepository<User>> mockUserRepo;
        private Mock<IRepository<Team>> mockTeamRepo;
        private Mock<IRepository<UsersTeams>> mockUTRepo;
        private Mock<IUserBusinessLogic> mockUserBL;
        private List<User> userList;

        [TestInitialize]
        public void TestInitialize()
        {
            userWithAllData = new User()
            {
                Name = "name",
                Username = "username",
                LastName = "lastname",
                Password = "hash",
                Email = "email@email.com",
                Role = Role.User,
                Token = Guid.NewGuid()
            };
            adminWithAllData = new User()
            {
                Name = "admin",
                Username = "admin",
                LastName = "lastname",
                Password = "hash",
                Email = "admin@email.com",
                Role = Role.Admin,
                Token = Guid.NewGuid()
            };
            mockUserRepo = new Mock<IRepository<User>>();
            mockTeamRepo = new Mock<IRepository<Team>>();
            mockUTRepo = new Mock<IRepository<UsersTeams>>();
            mockUserBL = new Mock<IUserBusinessLogic>();
            userList = new List<User>();
            userBLWithoutTeamBL = new UserBusinessLogic(mockUserRepo.Object, NO_BUSINESS_LOGIC, mockUTRepo.Object);
            mockUserRepo.Setup(r => r.Get(null, null, "")).Returns(userList);
            mockUserRepo.Setup(r => r.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(userList);
            mockUserRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(userWithAllData);
            mockUserBL.Setup(r => r.Login(It.IsAny<User>())).Returns(new User() { Token = Guid.NewGuid() });
            //userBLWithoutTeamBL.Login(adminWithAllData);
        }

        [TestMethod]
        public void AddUserOkTest()
        {
            mockUserRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => userList.Add(userWithAllData));
            userBLWithoutTeamBL.AddUser(userWithAllData);
            mockUserRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
        }

        [TestMethod]
        public void AddUserWithUsernameTest()
        {
            mockUserRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => userList.Add(userWithAllData));
            userBLWithoutTeamBL.AddUser(userWithAllData);
            mockUserRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
        }

        [TestMethod]
        public void AddUserWithFullInfoOkTest()
        {
            mockUserRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => userList.Add(userWithAllData));
            userBLWithoutTeamBL.AddUser(userWithAllData);
            mockUserRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
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
        [ExpectedException(typeof(EmailAlreadyRegisteredException))]
        public void AddUserWithNotUniqueEmailTest()
        {
            var user = new User() { Name = "Name", Username = "username", LastName = "Lastname", Password = "hash", Email = "something@domain.com" };
            var user2 = new User() { Name = "Name", Username = "username2", LastName = "Lastname", Password = "hash", Email = "something@domain.com" };
            mockUserRepo.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(x => userList.Add(user));
            userBLWithoutTeamBL.AddUser(user);
            userBLWithoutTeamBL.AddUser(user2);
            mockUserRepo.Verify(x => x.Insert(It.IsAny<User>()), Times.Once());
            mockUserRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void FollowTeamTest()
        {
            var team = new Team();
            var teamBL = new TeamBusinessLogic(mockTeamRepo.Object, NO_BUSINESS_LOGIC);
            var userBL = new UserBusinessLogic(mockUserRepo.Object, teamBL, mockUTRepo.Object);
            mockTeamRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(team);
            mockUserRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(userWithAllData);
            userBL.FollowTeam(userWithAllData.Id, team.Id);
            mockUserRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.AtLeastOnce);
            mockTeamRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(UserDoesNotExistException))]
        public void FollowTeamWithInvalidUserTest()
        {
            var user = new User() { Name = "Name", Username = "nick33", LastName = "surname", Password = "hash", Email = "a@a.com" };
            var team = new Team();
            var teamBL = new TeamBusinessLogic(mockTeamRepo.Object, NO_BUSINESS_LOGIC);
            var userBL = new UserBusinessLogic(mockUserRepo.Object, teamBL, mockUTRepo.Object);
            mockUserRepo.Reset();
            mockTeamRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(team);
            userBL.FollowTeam(user.Id, team.Id);
            mockTeamRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void UnfollowTeamTest()
        {
            var team = new Team() { Id = 0, Name = "Team", SportId = 1 };
            var teamBL = new TeamBusinessLogic(mockTeamRepo.Object, NO_BUSINESS_LOGIC);
            var userBL = new UserBusinessLogic(mockUserRepo.Object, teamBL, mockUTRepo.Object);
            mockTeamRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(team);
            mockUserRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(userWithAllData);
            var fav = new UsersTeams { TeamId = team.Id, Team = team, User = userWithAllData, UserId = userWithAllData.Id };
            mockUTRepo.Setup(r => r.Get(It.IsAny<Expression<Func<UsersTeams, bool>>>(), null, "")).Returns(new List<UsersTeams>() { fav });
            userBL.FollowTeam(userWithAllData.Id, team.Id);
            userBL.UnfollowTeam(userWithAllData.Id, team.Id);
            mockUserRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Exactly(2));
            mockTeamRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Exactly(2));
            mockUTRepo.Verify(x => x.Delete(fav), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(UserDoesNotFollowTeamException))]
        public void UnfollowTeamShouldReturnExceptionTest()
        {
            var team = new Team();
            var teamBL = new TeamBusinessLogic(mockTeamRepo.Object, NO_BUSINESS_LOGIC);
            var userBL = new UserBusinessLogic(mockUserRepo.Object, teamBL, mockUTRepo.Object);
            mockTeamRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(team);
            mockUserRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(userWithAllData);
            userBL.UnfollowTeam(userWithAllData.Id, 123);
            mockUserRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
            mockTeamRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
            mockUTRepo.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            userList.Add(adminWithAllData);
            userBLWithoutTeamBL.Login(adminWithAllData);
            mockUserRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(userWithAllData);
            userBLWithoutTeamBL.Update(userWithAllData);
            mockUserRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.AtLeastOnce);
            mockUserRepo.Verify(x => x.Update(It.IsAny<User>()), Times.AtLeastOnce);
        }

        [TestMethod]
        [ExpectedException(typeof(UserDoesNotExistException))]
        public void UpdateUserWithInvalidUserTest()
        {
            mockUserRepo.Reset();
            userBLWithoutTeamBL.Update(userWithAllData);
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

        [TestMethod]
        public void DeleteUserTest()
        {
            mockUserRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(userWithAllData);
            mockUserRepo.Setup(r => r.Delete(It.IsAny<int>())).Callback<object>(x => userList.Clear());
            userBLWithoutTeamBL.Delete(userWithAllData.Id);
            mockUserRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.AtLeastOnce);
            mockUserRepo.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(UserDoesNotExistException))]
        public void DeleteUserNotValidTest()
        {
            mockUserRepo.Reset();
            userBLWithoutTeamBL.Delete(userWithAllData.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(EmailOrPasswordException))]
        public void LoginWithInvalidCredentialsTest()
        {
            mockUserRepo.Setup(r => r.Get(It.IsAny<Expression<Func<User, bool>>>(), null, ""))
                .Returns(new List<User>(){new User()
                {
                    Name = "name",
                    Username = "username",
                    LastName = "lastname",
                    Password = "invalidPWHash",
                    Email = "invalid@email.com",
                    Role = Role.User
                } });
            userList.Add(userWithAllData);
            userBLWithoutTeamBL.Login(userWithAllData);
            mockUserRepo.Verify(x => x.Get(It.IsAny<Expression<Func<User, bool>>>(), null, ""), Times.AtLeastOnce);
        }

        [TestMethod]
        [ExpectedException(typeof(EmailOrPasswordException))]
        public void LoginWithInvalidPasswordTest()
        {
            mockUserRepo.Setup(r => r.Get(It.IsAny<Expression<Func<User, bool>>>(), null, ""))
                .Returns(new List<User>(){new User()
                {
                    Name = "admin",
                    Username = "admin",
                    LastName = "lastname",
                    Password = "invalidPsw",
                    Email = "admin@email.com",
                    Role = Role.User,
                } });
            userList.Add(userWithAllData);
            userBLWithoutTeamBL.Login(userWithAllData);
            mockUserRepo.Verify(x => x.Get(It.IsAny<Expression<Func<User, bool>>>(), null, ""), Times.AtLeastOnce);
        }

        [TestMethod]
        public void GetAllTest()
        {
            userBLWithoutTeamBL.GetAll();
            mockUserRepo.Verify(r => r.Get(null, null, ""), Times.Once);
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            var user = userBLWithoutTeamBL.GetById(userWithAllData.Id);
            mockUserRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.AtLeastOnce);
        }

        [TestMethod]
        [ExpectedException(typeof(UserDoesNotExistException))]
        public void GetUserByIdWithInvalidUserIdTest()
        {
            mockUserRepo.Reset();
            var user = userBLWithoutTeamBL.GetById(It.IsAny<int>());
            mockUserRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public void TokenIsValidTest()
        {
            mockUserRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(userWithAllData);
            var user = userBLWithoutTeamBL.TokenIsValid(It.IsAny<Guid>().ToString());
            mockUserRepo.Verify(x => x.Get(null, null, ""), Times.Once);
        }

        [TestMethod]
        public void TokenIsInvalidTest()
        {
            mockUserRepo.Reset();
            var user = userBLWithoutTeamBL.TokenIsValid(It.IsAny<Guid>().ToString());
            mockUserRepo.Verify(x => x.Get(null, null, ""), Times.Once);
        }

        [TestMethod]
        public void TokenIsInvalidSecondTestTest()
        {
            mockUserRepo.Setup(r => r.Get(null, null, "")).Returns(userList);
            userList.Add(userWithAllData);
            var user = userBLWithoutTeamBL.TokenIsValid("ab498bbf-ae0d-4d48-a0f1-e55bdad922a3");
            mockUserRepo.Verify(x => x.Get(null, null, ""), Times.Once);
        }

        [TestMethod]
        public void RepositoryIsDisposedTest()
        {
            userBLWithoutTeamBL.Dispose();
            mockUserRepo.Verify(x => x.Dispose(), Times.Once);
        }

        [TestMethod]
        public void LogoutTest()
        {
            mockUserRepo.Setup(r => r.Update(It.IsAny<User>())).Callback<User>(x => userList.First().Token = null);
            userList.Add(adminWithAllData);
            userBLWithoutTeamBL.Logout(adminWithAllData.Email);
            mockUserRepo.Verify(x => x.Get(It.IsAny<Expression<Func<User, bool>>>(), null, ""), Times.Once);
            mockUserRepo.Verify(x => x.Update(It.IsAny<User>()), Times.AtLeastOnce);
        }

        [TestMethod]
        [ExpectedException(typeof(UserDoesNotExistException))]
        public void LogoutWithInvalidUserTest()
        {
            userBLWithoutTeamBL.Logout(adminWithAllData.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(UserIsNotLoggedInException))]
        public void LogoutWithUserNotLoggedInTest()
        {
            mockUserRepo.Setup(r => r.Get(It.IsAny<Expression<Func<User, bool>>>(), null, "")).Returns(userList);
            adminWithAllData.Token = null;
            userList.Add(adminWithAllData);
            userBLWithoutTeamBL.Logout(adminWithAllData.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(UserDoesNotExistException))]
        public void LoginWithInvalidUserTest()
        {
            userBLWithoutTeamBL.Login(adminWithAllData);
        }

        [TestMethod]
        public void GetFavoritesOkTest()
        {
            mockUTRepo.Setup(r => r.Get(It.IsAny<Expression<Func<UsersTeams, bool>>>(), null, It.IsAny<string>())).Returns(new List<UsersTeams>());
            userBLWithoutTeamBL.GetFavoritesOfUser(adminWithAllData.Id);
            mockUTRepo.Verify(x => x.Get(It.IsAny<Expression<Func<UsersTeams, bool>>>(), null, It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(UserDoesNotExistException))]
        public void GetFavoritesUserDoesNotExistTest()
        {
            mockUserRepo.Reset();
            mockUTRepo.Setup(r => r.Get(It.IsAny<Expression<Func<UsersTeams, bool>>>(), null, It.IsAny<string>())).Returns(new List<UsersTeams>());
            userBLWithoutTeamBL.GetFavoritesOfUser(adminWithAllData.Id);
            mockUTRepo.Verify(x => x.Get(It.IsAny<Expression<Func<UsersTeams, bool>>>(), null, It.IsAny<string>()), Times.Once);
        }
    }
}
