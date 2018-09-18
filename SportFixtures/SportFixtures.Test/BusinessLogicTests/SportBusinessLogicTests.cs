using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.BusinessLogic.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportFixtures.Test.BusinessLogicTests
{
    [TestClass]
    public class SportBusinessLogicTests
    {

        [TestMethod]
        public void UniqueNameTest()
        {
            var mockRepo = new Mock<IRepository<Sport>>();
            mockRepo.Setup(un => un.Get(null, null, "")).Returns(new List<Sport>());
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockRepo.Object);
            string sportName = "Futbol";
            Assert.AreEqual(sportBL.UniqueName(sportName), true);
        }

        [TestMethod]
        public void AddSportTest()
        {
            var sportName = "Futbol";
            var list = new List<Sport>();
            var mockRepo = new Mock<IRepository<Sport>>();
            mockRepo.Setup(x => x.Insert(It.IsAny<Sport>())).Callback<Sport>(x => list.Add(new Sport() { Name = sportName }));
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockRepo.Object);
            sportBL.AddSport(sportName);
            mockRepo.Verify(x => x.Insert(It.IsAny<Sport>()), Times.Once());
            Assert.IsTrue(list.First().Name == sportName);
        }

    }
}
