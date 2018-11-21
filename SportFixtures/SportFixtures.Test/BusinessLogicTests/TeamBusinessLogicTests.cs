using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.DTOs;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.SportExceptions;
using SportFixtures.Exceptions.TeamExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using SportFixtures.Data.Enums;

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
        private List<Sport> sportList;

        [TestInitialize]
        public void TestInitialize()
        {
            teamWithAllData = new Team() { Name = "TeamName", Photo = @"C:\path\to\file.jpg", SportId = 1 };
            teamList = new List<Team>() { teamWithAllData };
            sport = new Sport() { Id = 1, Name = "SportName", Teams = teamList };
            sportList = new List<Sport>() { sport };
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
            mockSportRepo.Setup(r => r.Get(It.IsAny<Expression<Func<Sport, bool>>>(), null, "Teams")).Returns(sportList);
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
        public void UpdateTeamNameOkTest()
        {
            mockTeamRepo.Setup(x => x.Update(It.IsAny<Team>())).Callback<Team>(x => teamList.First().Name = teamWithAllData.Name);
            mockTeamRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(new Team() { Name = "TeamName", Photo = "C:\\path\\to\\file.jpg", SportId = 1 });
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
            mockSportRepo.Setup(r => r.Get(It.IsAny<Expression<Func<Sport, bool>>>(), null, "Teams")).Returns(sportList);
            teamBL.Add(teamWithAllData);
        }

        [TestMethod]
        public void GetAllTest()
        {
            teamBL.GetAll(null);
            mockTeamRepo.Verify(x => x.Get(null, null, ""), Times.Once());
        }

        [TestMethod]
        public void GetAllFilterByNameTest()
        {
            TeamFilterDTO filter = new TeamFilterDTO { Name = "Nacional" };
            mockTeamRepo.Setup(r => r.Get(It.IsAny<Expression<Func<Team, bool>>>(), It.IsAny<Func<IQueryable<Team>, IOrderedQueryable<Team>>>(), "")).Returns(teamList);
            teamBL.GetAll(filter);
            mockTeamRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Team, bool>>>(), It.IsAny<Func<IQueryable<Team>, IOrderedQueryable<Team>>>(), ""), Times.Once());
        }

        [TestMethod]
        public void GetAllOrderAscendingTest()
        {
            TeamFilterDTO filter = new TeamFilterDTO { Order = Order.Ascending };
            mockTeamRepo.Setup(r => r.Get(It.IsAny<Expression<Func<Team, bool>>>(), It.IsAny<Func<IQueryable<Team>, IOrderedQueryable<Team>>>(), "")).Returns(teamList);
            teamBL.GetAll(filter);
            mockTeamRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Team, bool>>>(), It.IsAny<Func<IQueryable<Team>, IOrderedQueryable<Team>>>(), ""), Times.Once());
        }

        [TestMethod]
        public void GetAllOrderDescendingTest()
        {
            TeamFilterDTO filter = new TeamFilterDTO { Order = Order.Descending };
            mockTeamRepo.Setup(r => r.Get(It.IsAny<Expression<Func<Team, bool>>>(), It.IsAny<Func<IQueryable<Team>, IOrderedQueryable<Team>>>(), "")).Returns(teamList);
            teamBL.GetAll(filter);
            mockTeamRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Team, bool>>>(), It.IsAny<Func<IQueryable<Team>, IOrderedQueryable<Team>>>(), ""), Times.Once());
        }

        [TestMethod]
        public void GetAllFilterByNameDescendingTest()
        {
            TeamFilterDTO filter = new TeamFilterDTO { Name = "Nacional", Order = Order.Descending };
            mockTeamRepo.Setup(r => r.Get(It.IsAny<Expression<Func<Team, bool>>>(), It.IsAny<Func<IQueryable<Team>, IOrderedQueryable<Team>>>(), "")).Returns(teamList);
            teamBL.GetAll(filter);
            mockTeamRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Team, bool>>>(), It.IsAny<Func<IQueryable<Team>, IOrderedQueryable<Team>>>(), ""), Times.Once());
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
