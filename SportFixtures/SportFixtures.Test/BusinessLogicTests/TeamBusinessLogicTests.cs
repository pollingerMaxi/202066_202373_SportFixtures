using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
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
        private Context context;
        private IRepository<Team> repository;

        [TestMethod]
        public void AddTeamOkTest()
        {
            var team = new Team() { Id = 1, Name = "Nacional", SportId = 1, PhotoPath = "c:\\path\\photo.jpg" };
            var sport = new Sport() { Id = 1, Name = "SportName" };
            var teamsList = new List<Team>();
            var mockTeamRepo = new Mock<IRepository<Team>>();
            var mockSportRepo = new Mock<IRepository<Sport>>();
            mockTeamRepo.Setup(x => x.Insert(It.IsAny<Team>())).Callback<Team>(x => teamsList.Add(team));
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockSportRepo.Object);
            ITeamBusinessLogic teamBL = new TeamBusinessLogic(mockTeamRepo.Object, sportBL);
            mockSportRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(sport);
            teamBL.AddTeam(team);
            mockTeamRepo.Verify(x => x.Insert(It.IsAny<Team>()), Times.Once());
            mockTeamRepo.Verify(x => x.Save(), Times.Once());
            Assert.IsTrue(teamsList.First().Id == team.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTeamNameException))]
        public void AddTeamEmptyNameShouldReturnExceptionTest()
        {
            var team = new Team() { Id = 1, Name = "" };
            var teamsList = new List<Team>();
            var mockTeamRepo = new Mock<IRepository<Team>>();
            var mockSportRepo = new Mock<IRepository<Sport>>();
            mockTeamRepo.Setup(x => x.Insert(It.IsAny<Team>())).Callback<Team>(x => teamsList.Add(team));
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockSportRepo.Object);
            ITeamBusinessLogic teamBL = new TeamBusinessLogic(mockTeamRepo.Object, sportBL);
            teamBL.AddTeam(team);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPhotoPathException))]
        public void AddTeamInvalidPhotoPathShouldReturnExceptionTest()
        {
            var team = new Team() { Id = 1, Name = "TeamName", PhotoPath = "!230#_skdpath" };
            var teamsList = new List<Team>();
            var mockTeamRepo = new Mock<IRepository<Team>>();
            var mockSportRepo = new Mock<IRepository<Sport>>();
            mockTeamRepo.Setup(x => x.Insert(It.IsAny<Team>())).Callback<Team>(x => teamsList.Add(team));
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockSportRepo.Object);
            ITeamBusinessLogic teamBL = new TeamBusinessLogic(mockTeamRepo.Object, sportBL);
            teamBL.AddTeam(team);
        }
    }
}
