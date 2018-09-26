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
    }
}
