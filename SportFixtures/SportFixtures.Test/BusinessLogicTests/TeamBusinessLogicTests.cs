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
            var team = new Team() { Id = 1, Name = "Nacional" };
            var teamsList = new List<Team>();
            var mockTeamRepo = new Mock<IRepository<Team>>();
            var mockSportRepo = new Mock<IRepository<Sport>>();
            mockTeamRepo.Setup(x => x.Insert(It.IsAny<Team>())).Callback<Team>(x => teamsList.Add(team));
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockSportRepo.Object);
            ITeamBusinessLogic teamBL = new TeamBusinessLogic(mockTeamRepo.Object, sportBL);

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
        public void TeamExistsByIdShouldReturnFalseTest()
        {
            var team = new Team() { Id = 1, Name = "Nacional" };
            var teamsList = new List<Team>();
            var mockTeamRepo = new Mock<IRepository<Team>>();
            var mockSportRepo = new Mock<IRepository<Sport>>();
            mockTeamRepo.Setup(x => x.Insert(It.IsAny<Team>())).Callback<Team>(x => teamsList.Add(team));
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockSportRepo.Object);
            ITeamBusinessLogic teamBL = new TeamBusinessLogic(mockTeamRepo.Object, sportBL);
            mockTeamRepo.Setup(un => un.Get(null, null, "")).Returns(teamsList);
            teamBL.AddTeam(team);
            Assert.IsTrue(!teamBL.TeamExistsById(2));
        }

        [TestMethod]
        public void TeamExistsByIdShouldReturnTrueTest()
        {
            var team = new Team() { Id = 1, Name = "Nacional" };
            var teamsList = new List<Team>();
            var mockTeamRepo = new Mock<IRepository<Team>>();
            var mockSportRepo = new Mock<IRepository<Sport>>();
            mockTeamRepo.Setup(x => x.Insert(It.IsAny<Team>())).Callback<Team>(x => teamsList.Add(team));
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockSportRepo.Object);
            ITeamBusinessLogic teamBL = new TeamBusinessLogic(mockTeamRepo.Object, sportBL);
            mockTeamRepo.Setup(un => un.Get(null, null, "")).Returns(teamsList);
            teamBL.AddTeam(team);
            Assert.IsTrue(teamBL.TeamExistsById(team.Id));
        }

        [TestMethod]
        public void UpdateSportIdOfTeamTest()
        {
            var team = new Team() { Id = 1, Name = "Nacional" };
            var sport = new Sport() { Id = 1, Name = "Sport" };
            var teamsList = new List<Team>();
            var mockTeamRepo = new Mock<IRepository<Team>>();
            var mockSportRepo = new Mock<IRepository<Sport>>();
            mockTeamRepo.Setup(x => x.Insert(It.IsAny<Team>())).Callback<Team>(x => teamsList.Add(team));
            mockTeamRepo.Setup(x => x.Update(It.IsAny<Team>())).Callback<Team>(x => teamsList.First().Id = sport.Id);
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockSportRepo.Object);
            ITeamBusinessLogic teamBL = new TeamBusinessLogic(mockTeamRepo.Object, sportBL);
            mockTeamRepo.Setup(un => un.Get(null, null, "")).Returns(teamsList);
            teamBL.AddTeam(team);
            teamBL.AddTeamToSport(team);
            mockTeamRepo.Verify(x => x.Update(team), Times.Once);
        }


    }
}
