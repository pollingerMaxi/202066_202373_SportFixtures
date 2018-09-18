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
using SportFixtures.Exceptions.SportExceptions;

namespace SportFixtures.Test.BusinessLogicTests
{
    [TestClass]
    public class SportBusinessLogicTests
    {

        [TestMethod]
        public void UniqueNameTest()
        {
            var sports = new List<Sport>();
            sports.Add(new Sport{Name="Basquetbol"});
            var mockRepo = new Mock<IRepository<Sport>>();
            mockRepo.Setup(un => un.Get(null, null, "")).Returns(sports);
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockRepo.Object);
            string sportName = "Futbol";
            Assert.AreEqual(true, sportBL.UniqueName(sportName));
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicatedSportNameException))]
        public void NotUniqueNameTest()
        {
            var sports = new List<Sport>();
            sports.Add(new Sport{Name="Futbol"});
            var mockRepo = new Mock<IRepository<Sport>>();
            mockRepo.Setup(un => un.Get(null, null, "")).Returns(sports);
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockRepo.Object);
            string sportName = "Futbol";
            Assert.AreEqual(false, sportBL.UniqueName(sportName));
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
            mockRepo.Verify(x => x.Save(), Times.Once());
            Assert.IsTrue(list.First().Name == sportName);
        }

        [TestMethod]
        public void AddSportWithDuplicatedNameTest()
        {
            var sportName = "Futbol";
            var list = new List<Sport>();
            var mockRepo = new Mock<IRepository<Sport>>();
            mockRepo.Setup(x => x.Insert(It.IsAny<Sport>())).Callback<Sport>(x => list.Add(new Sport() { Name = sportName }));
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockRepo.Object);
            sportBL.AddSport(sportName);
            mockRepo.Verify(x => x.Insert(It.IsAny<Sport>()), Times.Once());
            mockRepo.Verify(x => x.Save(), Times.Once());
            sportBL.AddSport(sportName);
            mockRepo.Verify(x => x.Insert(It.IsAny<Sport>()), Times.Exactly(2));
            mockRepo.Verify(x => x.Save(), Times.Exactly(2));
            Assert.IsTrue(list.First().Name == sportName);
        }

    }
}
