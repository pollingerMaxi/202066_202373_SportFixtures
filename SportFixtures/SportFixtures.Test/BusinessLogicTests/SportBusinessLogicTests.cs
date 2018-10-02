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
        private Mock<IRepository<Sport>> mockSportRepo;
        private ISportBusinessLogic sportBL;
        private List<Sport> sportList;

        [TestInitialize]
        public void TestInitialize()
        {
            mockSportRepo = new Mock<IRepository<Sport>>();
            sportBL = new SportBusinessLogic(mockSportRepo.Object);
            sportList = new List<Sport>();
            mockSportRepo.Setup(un => un.Get(null, null, "")).Returns(sportList);
        }

        [TestMethod]
        public void AddSportUniqueNameOkTest()
        {
            sportList.Add(new Sport { Name = "Basquetbol" });
            Assert.IsTrue(sportBL.GetAll().Count() == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicatedSportNameException))]
        public void AddTeamNotUniqueNameShouldReturnExceptionTest()
        {
            Sport sport = new Sport(){ Name = "Futbol" };
            sportList.Add(sport);
            sportBL.Add(sport);
        }

        [TestMethod]
        public void AddSportOkTest()
        {
            var sportName = "Futbol";
            mockSportRepo.Setup(x => x.Insert(It.IsAny<Sport>())).Callback<Sport>(x => sportList.Add(new Sport() { Name = sportName }));
            sportBL.Add(new Sport() { Name = sportName });
            mockSportRepo.Verify(x => x.Insert(It.IsAny<Sport>()), Times.Once());
            mockSportRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicatedSportNameException))]
        public void AddSportWithDuplicatedNameTest()
        {
            var sportName = "Futbol";
            var sport = new Sport() { Name = sportName };
            mockSportRepo.Setup(x => x.Insert(It.IsAny<Sport>())).Callback<Sport>(x => sportList.Add(sport));
            sportBL.Add(sport);
            mockSportRepo.Verify(x => x.Insert(It.IsAny<Sport>()), Times.Once());
            mockSportRepo.Verify(x => x.Save(), Times.Once());
            sportBL.Add(sport);
            mockSportRepo.Verify(x => x.Insert(It.IsAny<Sport>()), Times.Exactly(2));
            mockSportRepo.Verify(x => x.Save(), Times.Exactly(2));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSportNameException))]
        public void AddSportWithoutNameTest()
        {
            var sportName = "";
            sportBL.Add(new Sport() { Name = sportName });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSportNameException))]
        public void AddSportNameOnlySpacesTest()
        {
            var sportName = "           ";
            sportBL.Add(new Sport() { Name = sportName });
        }

        [TestMethod]
        public void AddTeamToSportOkTest()
        {
            var team = new Team() { Name = "TeamName", SportId = 34 };
            var sport = new Sport() { Name = "SportName", Id = 34 };
            sportList.Add(sport);
            mockSportRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(sport);
            mockSportRepo.Setup(r => r.Update(It.IsAny<Sport>())).Callback<Sport>(x => sportList.First().Teams.Add(team));
            sportBL.AddTeamToSport(team);
            mockSportRepo.Verify(r => r.GetById(team.SportId), Times.AtLeast(2));
            mockSportRepo.Verify(r => r.Update(It.IsAny<Sport>()), Times.Once);
        }

        [TestMethod]
        public void TeamDoesntExistsOnASportList()
        {
            var team = new Team() { Id = '1', Name = "Nacional" };
            var sport = new Sport() { Name = "SportName" };
            sportList.Add(sport);
            var mockTeamRepo = new Mock<IRepository<Team>>();
            ITeamBusinessLogic teamBl = new TeamBusinessLogic(mockTeamRepo.Object, sportBL);
            mockSportRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(sport);
            mockSportRepo.Setup(r => r.Update(It.IsAny<Sport>())).Callback<Sport>(x => sportList.First().Teams.Add(team));
            sportBL.AddTeamToSport(team);
            mockSportRepo.Verify(x => x.Update(sport), Times.Once);
            mockSportRepo.Verify(x => x.Save(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(TeamAlreadyInSportException))]
        public void TeamExistsOnASportList()
        {
            var team = new Team() { Id = '1', Name = "Nacional" };
            var sport = new Sport() { Name = "SportName" };
            sportList.Add(sport);
            var mockTeamRepo = new Mock<IRepository<Team>>();
            ITeamBusinessLogic teamBl = new TeamBusinessLogic(mockTeamRepo.Object, sportBL);
            mockSportRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(sport);
            mockSportRepo.Setup(r => r.Update(It.IsAny<Sport>())).Callback<Sport>(x => sportList.First().Teams.Add(team));
            sportBL.AddTeamToSport(team);
            sportBL.AddTeamToSport(team);
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

        [TestMethod]
        [ExpectedException(typeof(SportDoesNotExistException))]
        public void AddTeamTosportListhouldReturnExceptionTest()
        {
            var team = new Team() { Name = "TeamName", SportId = 4 };
            var sport = new Sport() { Name = "SportName", Id = 34 };
            sportList.Add(sport);
            mockSportRepo.Setup(r => r.Update(It.IsAny<Sport>())).Callback<Sport>(x => sportList.First().Teams.Add(team));
            sportBL.AddTeamToSport(team);
            mockSportRepo.Verify(r => r.GetById(team.SportId), Times.Once);
            mockSportRepo.Verify(r => r.Update(It.IsAny<Sport>()), Times.Once);
        }

        [TestMethod]
        public void UpdateSportOkTest()
        {
            Sport sport = new Sport(){Id = 1, Name = "Futbol"};
            mockSportRepo.Setup(x => x.Insert(It.IsAny<Sport>())).Callback<Sport>(x => sportList.Add(sport));
            mockSportRepo.Setup(x => x.Update(It.IsAny<Sport>())).Callback<Sport>(x => sportList.First().Name = sport.Name);
            mockSportRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(sport);
            sportBL.Add(sport);
            sport.Name = "UpdatedName";
            sportBL.Update(sport);
            mockSportRepo.Verify(x => x.Insert(It.IsAny<Sport>()), Times.Once());
            mockSportRepo.Verify(x => x.Update(It.IsAny<Sport>()), Times.Once());
            mockSportRepo.Verify(x => x.Save(), Times.AtLeastOnce());
            Assert.IsTrue(sportList.First().Name == "UpdatedName");
        }

        [TestMethod]
        [ExpectedException(typeof(SportDoesNotExistException))]
        public void UpdatesportListhouldReturnExceptionTest()
        {
            Sport sport = new Sport(){Id = 1, Name = "Futbol"};
            mockSportRepo.Setup(x => x.Insert(It.IsAny<Sport>())).Callback<Sport>(x => sportList.Add(sport));
            mockSportRepo.Setup(x => x.Update(It.IsAny<Sport>())).Callback<Sport>(x => sportList.First().Name = sport.Name);
            sport.Name = "UpdatedName";
            sportBL.Update(sport);
            mockSportRepo.Verify(x => x.Insert(It.IsAny<Sport>()), Times.Once());
            mockSportRepo.Verify(x => x.Update(It.IsAny<Sport>()), Times.Once());
            mockSportRepo.Verify(x => x.Save(), Times.AtLeastOnce());
            Assert.IsTrue(sportList.First().Name == "UpdatedName");
        }

        [TestMethod]
        public void DeleteSportOkTest()
        {
            Sport sport = new Sport(){Id = 1, Name = "Futbol"};
            mockSportRepo.Setup(x => x.Insert(It.IsAny<Sport>())).Callback<Sport>(x => sportList.Add(sport));
            mockSportRepo.Setup(x => x.Delete(It.IsAny<Sport>())).Callback<Sport>(x => sportList.Remove(sport));
            mockSportRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(sport);
            sportBL.Add(sport);
            sportBL.Delete(sport);
            mockSportRepo.Verify(x => x.Insert(It.IsAny<Sport>()), Times.Once());
            mockSportRepo.Verify(x => x.Delete(It.IsAny<Sport>()), Times.Once());
            mockSportRepo.Verify(x => x.Save(), Times.AtLeastOnce());
            Assert.IsTrue(sportList.Count() == 0);
        }

        [TestMethod]
        public void GetAllTest()
        {
            sportBL.GetAll();
            mockSportRepo.Verify(x => x.Get(null, null, ""), Times.Once());
        }
    }
}
