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
            sports.Add(new Sport { Name = "Basquetbol" });
            var mockRepo = new Mock<IRepository<Sport>>();
            mockRepo.Setup(un => un.Get(null, null, "")).Returns(sports);
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockRepo.Object);
            Assert.IsTrue(sports.Count == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicatedSportNameException))]
        public void NotUniqueNameTest()
        {
            var sports = new List<Sport>();
            sports.Add(new Sport { Name = "Futbol" });
            var mockRepo = new Mock<IRepository<Sport>>();
            mockRepo.Setup(un => un.Get(null, null, "")).Returns(sports);
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockRepo.Object);
            sportBL.ValidateSport(new Sport { Name = "Futbol" });
        }

        [TestMethod]
        public void AddSportOkTest()
        {
            var sportName = "Futbol";
            var list = new List<Sport>();
            var mockRepo = new Mock<IRepository<Sport>>();
            mockRepo.Setup(x => x.Insert(It.IsAny<Sport>())).Callback<Sport>(x => list.Add(new Sport() { Name = sportName }));
            mockRepo.Setup(x => x.Get(null, null, "")).Returns(list);
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockRepo.Object);
            sportBL.AddSport(new Sport() { Name = sportName });
            mockRepo.Verify(x => x.Insert(It.IsAny<Sport>()), Times.Once());
            mockRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicatedSportNameException))]
        public void AddSportWithDuplicatedNameTest()
        {
            var sportName = "Futbol";
            var list = new List<Sport>();
            var mockRepo = new Mock<IRepository<Sport>>();
            var sport = new Sport() { Name = sportName };
            mockRepo.Setup(x => x.Insert(It.IsAny<Sport>())).Callback<Sport>(x => list.Add(sport));
            mockRepo.Setup(x => x.Get(null, null, "")).Returns(list);
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockRepo.Object);
            sportBL.AddSport(sport);
            mockRepo.Verify(x => x.Insert(It.IsAny<Sport>()), Times.Once());
            mockRepo.Verify(x => x.Save(), Times.Once());
            sportBL.AddSport(sport);
            mockRepo.Verify(x => x.Insert(It.IsAny<Sport>()), Times.Exactly(2));
            mockRepo.Verify(x => x.Save(), Times.Exactly(2));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSportNameException))]
        public void AddSportWithoutNameTest()
        {
            var sportName = "";
            var list = new List<Sport>();
            var mockRepo = new Mock<IRepository<Sport>>();
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockRepo.Object);
            sportBL.AddSport(new Sport() { Name = sportName });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSportNameException))]
        public void AddSportNameOnlySpacesTest()
        {
            var sportName = "           ";
            var list = new List<Sport>();
            var mockRepo = new Mock<IRepository<Sport>>();
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockRepo.Object);
            sportBL.AddSport(new Sport() { Name = sportName });
        }

        [TestMethod]
        public void AddTeamToSportOkTest()
        {
            var team = new Team();
            var sport = new Sport() { Name = "SportName" };
            var list = new List<Sport>();
            var mockRepo = new Mock<IRepository<Sport>>();
            mockRepo.Setup(r => r.Insert(It.IsAny<Sport>())).Callback<Sport>(x => list.Add(sport));
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockRepo.Object);
            sportBL.AddTeamToSport(team, sport);
            mockRepo.Verify(x => x.Update(It.IsAny<Sport>()), Times.Once);
        }

        [TestMethod]
        public void TeamDoesntExistsOnASportList()
        {
            var team = new Team() { Id = '1', Name = "Nacional" };
            var sport = new Sport() { Name = "SportName" };
            var sportsList = new List<Sport>() { sport };
            var mockSportRepo = new Mock<IRepository<Sport>>();
            var mockTeamRepo = new Mock<IRepository<Team>>();
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockSportRepo.Object);
            ITeamBusinessLogic teamBl = new TeamBusinessLogic(mockTeamRepo.Object, sportBL);
            mockSportRepo.Setup(r => r.Update(It.IsAny<Sport>())).Callback<Sport>(x => sportsList.First().Teams.Add(team));
            sportBL.AddTeamToSport(team, sport);
            mockSportRepo.Verify(x => x.Update(sport), Times.Once);
            mockSportRepo.Verify(x => x.Save(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(TeamAlreadyInSportException))]
        public void TeamExistsOnASportList()
        {
            var team = new Team() { Id = '1', Name = "Nacional" };
            var sport = new Sport() { Name = "SportName" };
            var sportsList = new List<Sport>() { sport };
            var mockSportRepo = new Mock<IRepository<Sport>>();
            var mockTeamRepo = new Mock<IRepository<Team>>();
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockSportRepo.Object);
            ITeamBusinessLogic teamBl = new TeamBusinessLogic(mockTeamRepo.Object, sportBL);
            mockSportRepo.Setup(r => r.Update(It.IsAny<Sport>())).Callback<Sport>(x => sportsList.First().Teams.Add(team));
            sportBL.AddTeamToSport(team, sport);
            sportBL.AddTeamToSport(team, sport);
            mockSportRepo.Verify(x => x.Update(sport), Times.Exactly(2));
            mockSportRepo.Verify(x => x.Save(), Times.Exactly(2));
        }

        [TestMethod]
        public void EqualsShouldBeTrueTest()
        {
            var sportName = "SportName";
            var firstSport = new Sport() { Name = sportName };
            var secondSport = new Sport() { Name = sportName };
            Assert.AreEqual(firstSport, secondSport);
        }

        [TestMethod]
        public void EqualsShouldBeFalseTest()
        {
            var firstSport = new Sport() { Name = "SomeName" };
            var secondSport = new Sport() { Name = "SomeOtherName" };
            Assert.AreNotEqual(firstSport, secondSport);
        }

        [TestMethod]
        public void EqualsShouldBeFalse2Test()
        {
            var firstSport = new Sport() { Name = "SomeName" };
            var secondSport = new Sport() { Name = "SomeOtherName" };
            Assert.IsFalse(secondSport.Equals(null));
        }
    }
}
