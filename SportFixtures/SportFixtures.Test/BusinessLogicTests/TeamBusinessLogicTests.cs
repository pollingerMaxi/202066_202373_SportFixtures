using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.SportExceptions;
using SportFixtures.Exceptions.TeamExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportFixtures.Test.BusinessLogicTests
{
    [TestClass]
    public class TeamBusinessLogicTests
    {
        private const dynamic NO_BUSINESS_LOGIC = null;
        private Team teamWithAllData;
        private Sport sport;
        private Mock<IRepository<Sport>> mockSportRepo;
        private Mock<IRepository<Team>> mockTeamRepo;
        private ISportBusinessLogic sportBL;
        private ITeamBusinessLogic teamBL;
        private List<Team> teamList;

        [TestInitialize]
        public void TestInitialize()
        {
            teamWithAllData = new Team() { Name = "TeamName", PhotoPath = @"C:\path\to\file.jpg", SportId = 1 };
            teamList = new List<Team>() { teamWithAllData };
            sport = new Sport() { Id = 1, Name = "SportName", Teams = teamList, IsDeleted = false };
            mockTeamRepo = new Mock<IRepository<Team>>();
            mockSportRepo = new Mock<IRepository<Sport>>();
            sportBL = new SportBusinessLogic(mockSportRepo.Object);
            teamBL = new TeamBusinessLogic(mockTeamRepo.Object, sportBL);
            mockTeamRepo.Setup(r => r.Get(null, null, "")).Returns(teamList);
            mockTeamRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(teamWithAllData);
            mockSportRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(sport);
        }

        [TestMethod]
        public void AddTeamOkTest()
        {
            var team = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "SportName" };
            var teamsList = new List<Team>();
            mockTeamRepo.Setup(x => x.Insert(It.IsAny<Team>())).Callback<Team>(x => teamsList.Add(team));
            teamBL.Add(team);
            mockTeamRepo.Verify(x => x.Insert(It.IsAny<Team>()), Times.Once());
            mockTeamRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTeamNameException))]
        public void AddTeamEmptyNameShouldReturnExceptionTest()
        {
            var team = new Team() { Id = 1, Name = "" };
            var teamsList = new List<Team>();
            teamBL.Add(team);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPhotoPathException))]
        public void AddTeamInvalidPhotoPathShouldReturnExceptionTest()
        {
            var team = new Team() { Id = 1, Name = "TeamName", PhotoPath = "!230#_skdpath" };
            var teamsList = new List<Team>();
            teamBL.Add(team);
        }

        [TestMethod]
        public void UpdateTeamNameOkTest()
        {
            mockTeamRepo.Setup(x => x.Update(It.IsAny<Team>())).Callback<Team>(x => teamList.First().Name = teamWithAllData.Name);
            mockTeamRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(new Team() { Name = "TeamName", PhotoPath = "C:\\path\\to\\file.jpg", SportId = 1 });
            teamWithAllData.Name = "UpdatedName";
            teamBL.Update(teamWithAllData);
            mockTeamRepo.Verify(x => x.Update(It.IsAny<Team>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(TeamDoesNotExistsException))]
        public void UpdateTeamNameShouldReturnExceptionTest()
        {
            mockTeamRepo.Reset();
            teamBL.Update(teamWithAllData);
        }

        [TestMethod]
        public void DeleteTeamOkTest()
        {
            mockTeamRepo.Setup(x => x.Delete(It.IsAny<int>())).Callback<object>(x => teamList.Remove(teamWithAllData));
            teamBL.Delete(teamWithAllData.Id);
            mockTeamRepo.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(TeamDoesNotExistsException))]
        public void DeleteTeamWithInvalidTeamTest()
        {
            mockTeamRepo.Reset();
            teamBL.Delete(teamWithAllData.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(TeamAlreadyInSportException))]
        public void AddTeamInASportThatAlreadyHasTheTeamTest()
        {
            //TestInitialize already adds this team to the list and has a mock.setup configured 
            //to return the sport and the list of teams
            teamBL.Add(teamWithAllData);
        }

        [TestMethod]
        public void GetAllTest()
        {
            teamBL.GetAll();
            mockTeamRepo.Verify(x => x.Get(null, null, ""), Times.Once());
        }

        [TestMethod]
        public void EqualsTest()
        {
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            Assert.IsFalse(team1.Equals(null));
        }

        [TestMethod]
        public void GetTeamByIdTest()
        {
            var team = teamBL.GetById(teamWithAllData.Id);
            mockTeamRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(TeamDoesNotExistsException))]
        public void GetTeamByIdWithInvalidTeamIdTest()
        {
            mockTeamRepo.Reset();
            var team = teamBL.GetById(99999999);
            mockTeamRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }
    }
}
