using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.FixtureGenerator;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.EncounterExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using SportFixtures.Data;

namespace SportFixtures.Test.BusinessLogicTests
{
    [TestClass]
    public class EncounterBusinessLogicTests
    {
        private const dynamic NO_BUSINESS_LOGIC = null;
        private Mock<IRepository<Encounter>> mockEncounterRepo;
        private Mock<IRepository<Sport>> mockSportRepo;
        private Mock<ISportBusinessLogic> mockSportBL;
        private IEncounterBusinessLogic encounterBL;
        private ISportBusinessLogic sportBL;
        private List<Encounter> encounterList;
        private Sport football;
        private Sport footballWithTeams;
        private Team nacional;
        private Team peñarol;
        private Team cerro;
        private Team defensor;
        private Team danubio;

        private Sport basketball;
        private Team aguada;

        [TestInitialize]
        public void TestInitialize()
        {
            mockEncounterRepo = new Mock<IRepository<Encounter>>();
            mockSportRepo = new Mock<IRepository<Sport>>();
            sportBL = new SportBusinessLogic(mockSportRepo.Object);
            mockSportBL = new Mock<ISportBusinessLogic>();
            encounterBL = new EncounterBusinessLogic(mockEncounterRepo.Object, mockSportBL.Object);
            encounterList = new List<Encounter>();
            mockEncounterRepo.Setup(r => r.Get(null, null, It.IsAny<string>())).Returns(encounterList);
            mockEncounterRepo.Setup(r => r.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, "")).Returns(encounterList);

            football = new Sport() { Id = 1, Name = "Football", EncounterMode = EncounterMode.Double };
            mockSportBL.Setup(s => s.GetById(1)).Returns(football);

            basketball = new Sport() { Id = 2, Name = "Basketball", EncounterMode = EncounterMode.Double };
            mockSportBL.Setup(s => s.GetById(2)).Returns(football);

            nacional = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            peñarol = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            cerro = new Team() { Id = 3, Name = "Cerro", SportId = 1 };
            defensor = new Team { Id = 4, Name = "Defensor", SportId = 1 };
            danubio = new Team { Id = 5, Name = "Danubio", SportId = 1 };

            var teamList = new List<Team>() { nacional, peñarol, defensor, danubio, cerro };
            footballWithTeams = new Sport() { Id = 3, Name = "Football", EncounterMode = EncounterMode.Double, Teams = teamList };
            mockSportBL.Setup(s => s.GetById(3)).Returns(footballWithTeams);

            aguada = new Team() { Id = 2, Name = "Aguada", SportId = 2 };
        }

        [TestMethod]
        public void AddEncounterOkTest()
        {
            ICollection<Team> teams = new List<Team>(){nacional, peñarol};
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams};
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            
            encounterBL.Add(encounter);
            mockEncounterRepo.Verify(x => x.Insert(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterSameTeamException))]
        public void AddEncounterShouldReturnSameTeamExceptionTest()
        {   
            ICollection<Team> teams = new List<Team>(){nacional, nacional};
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterTeamsCantBeNullException))]
        public void AddEncounterWithoutTeamsShouldReturnNullTeamsExceptionTest()
        {
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterTeamsCantBeNullException))]
        public void AddEncounterWithoutOneTeamShouldReturnNullTeamsExceptionTest()
        {
            ICollection<Team> teams = new List<Team>(){nacional};
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterTeamsDifferentSportException))]
        public void AddEncounterWithTeamsFromDifferentSportsShouldReturnExceptionTest()
        {

            ICollection<Team> teams = new List<Team>(){nacional, aguada};
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterSportDifferentFromTeamsSportException))]
        public void AddEncounterWithDifferentSportAsTeamsSportShouldReturnExceptionTest()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = basketball.Id, Teams = teams};
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
        }

        [TestMethod]
        public void UpdateEncounterTeamOkTest()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol};
            ICollection<Team> updatedTeamList = new List<Team>() { cerro, peñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(x => x.Update(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.First().Teams = updatedTeamList);
            mockEncounterRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(encounter);
            encounterBL.Add(encounter);
            encounter.Teams = updatedTeamList;
            encounterBL.Update(encounter);
            mockEncounterRepo.Verify(x => x.Insert(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Update(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Save(), Times.AtLeast(2));
        }

        [TestMethod]
        public void UpdateEncounterDateOkTest()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol };
            DateTime updatedDate = new DateTime(2018, 9, 27, 8, 30, 00);
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(x => x.Update(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.First().Date = updatedDate);
            mockEncounterRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(encounter);
            encounterBL.Add(encounter);
            encounter.Date = updatedDate;
            encounterBL.Update(encounter);
            mockEncounterRepo.Verify(x => x.Insert(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Update(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Save(), Times.AtLeast(2));
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterTeamsDifferentSportException))]
        public void UpdateEncounterTeamWithDifferentSportShouldReturnsExceptionTest()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol };
            ICollection<Team> teamsDifferentSports = new List<Team>() { nacional, aguada };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(encounter);
            encounterBL.Add(encounter);
            encounter.Teams = teamsDifferentSports;
            encounterBL.Update(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterSportDifferentFromTeamsSportException))]
        public void UpdateEncounterSportWithDifferentTeamSportsShouldReturnsExceptionTest()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(encounter);
            encounterBL.Add(encounter);
            encounter.SportId = basketball.Id;
            encounterBL.Update(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterDoesNotExistException))]
        public void UpdateEncounterThatDoesntExistsReturnsExceptionTest()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            var notAddedEncounter = new Encounter() { Id = 2, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
            encounterBL.Update(notAddedEncounter);
        }

        [TestMethod]
        public void DeleteEncounterOkTest()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(x => x.Delete(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.RemoveAt(0));
            mockEncounterRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(encounter);
            encounterBL.Add(encounter);
            encounterBL.Delete(encounter.Id);
            mockEncounterRepo.Verify(x => x.Insert(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Save(), Times.AtLeast(2));
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterDoesNotExistException))]
        public void DeleteEncounterThatDoesntExistsReturnsExceptionTest()
        {
            encounterBL.Delete(1);
        }

        [TestMethod]
        public void CheckIfTeamsHaveAnEncounterOnTheSameDateOkTest()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol };
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            DateTime date2 = new DateTime(2018, 9, 28, 8, 30, 00);
            var encounter = new Encounter() { Id = 1, Date = date, SportId = football.Id, Teams = teams };
            var encounter2 = new Encounter() { Id = 2, Date = date2, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(encounter)).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(x => x.Insert(encounter2)).Callback<Encounter>(x => encounterList.Add(encounter2));
            encounterBL.Add(encounter);
            mockEncounterRepo.Verify(x => x.Insert(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Save(), Times.Once());
            encounterBL.Add(encounter2);
        }

        [TestMethod]
        [ExpectedException(typeof(TeamAlreadyHasAnEncounterOnTheSameDayException))]
        public void CheckIfTeamsHaveAnEncounterOnTheSameDateShouldReturnExceptionTest()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol };
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            var encounter = new Encounter() { Id = 1, Date = date, SportId = football.Id, Teams = teams };
            var encounter2 = new Encounter() { Id = 2, Date = date, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(encounter)).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(x => x.Insert(encounter2)).Callback<Encounter>(x => encounterList.Add(encounter2));
            encounterBL.Add(encounter);
            mockEncounterRepo.Verify(x => x.Insert(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Save(), Times.Once());
            encounterBL.Add(encounter2);
        }

        [TestMethod]
        [ExpectedException(typeof(TeamAlreadyHasAnEncounterOnTheSameDayException))]
        public void CheckTeamHasEncounterSameDateShouldReturnExceptionTest()
        {
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            DateTime date2 = new DateTime(2018, 9, 28, 8, 30, 00);
            ICollection<Team> encounterTeams = new List<Team>() { nacional, peñarol };
            ICollection<Team> encounter2Teams = new List<Team>() { cerro, peñarol };
            var encounter = new Encounter() { Id = 1, Date = date, SportId = football.Id, Teams = encounterTeams };
            var encounter2 = new Encounter() { Id = 2, Date = date2, SportId = football.Id, Teams = encounterTeams };
            var encounter3 = new Encounter() { Id = 3, Date = date2, SportId = football.Id, Teams = encounter2Teams };
            mockEncounterRepo.Setup(x => x.Insert(encounter)).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(x => x.Insert(encounter2)).Callback<Encounter>(x => encounterList.Add(encounter2));
            encounterBL.Add(encounter);
            encounterBL.Add(encounter2);
            encounterBL.Add(encounter3);
        }

        [TestMethod]
        public void GetAllTest()
        {
            encounterBL.GetAll();
            mockEncounterRepo.Verify(x => x.Get(null, null, ""), Times.Once());
        }

        [TestMethod]
        public void GetAllEncountersOfSportTest()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            encounterList.Add(encounter);
            int sportId = 1;
            encounterBL.GetAllEncountersOfSport(sportId);
            mockEncounterRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, ""), Times.Once());
        }

        [TestMethod]
        public void GetAllEncountersOfTeamTest()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            encounterList.Add(encounter);
            encounterBL.GetAllEncountersOfTeam(nacional.Id);
            mockEncounterRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, ""), Times.Once());
        }

        [TestMethod]
        public void GetAllEncountersOfTheDay()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            encounterList.Add(encounter);
            DateTime date = DateTime.Now;
            encounterBL.GetAllEncountersOfTheDay(date);
            mockEncounterRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, ""), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(NoEncountersFoundForSportException))]
        public void GetAllEncountersOfSportShouldReturnExceptionTest()
        {
            mockEncounterRepo.Reset();
            encounterBL.GetAllEncountersOfSport(It.IsAny<int>());
            mockEncounterRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, ""), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(NoEncountersFoundForTeamException))]
        public void GetAllEncountersOfTeamShouldReturnExceptionTest()
        {
            encounterBL.GetAllEncountersOfTeam(It.IsAny<int>());
            mockEncounterRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, ""), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(NoEncountersFoundForDateException))]
        public void GetAllEncountersOfTheDayShouldReturnExceptionTest()
        {
            encounterBL.GetAllEncountersOfTheDay(It.IsAny<DateTime>());
            mockEncounterRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, ""), Times.Once());
        }

        [TestMethod]
        public void GetEncounterByIdTest()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(encounter);
            var encounterFromDb = encounterBL.GetById(It.IsAny<int>());
            mockEncounterRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }


        [TestMethod]
        [ExpectedException(typeof(EncounterDoesNotExistException))]
        public void GetEncounterByIdWithInvalidEncounterTest()
        {
            var encounterFromDb = encounterBL.GetById(It.IsAny<int>());
            mockEncounterRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void CheckTeamsHaveEncountersOnTheSameDayOnAGivenListShouldReturnFalse()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol };
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            ICollection<Encounter> encounterList = new List<Encounter>();
            Encounter encounter = new Encounter() { Id = 1, Teams = teams, SportId = football.Id, Date = date };
            Encounter encounter2 = new Encounter() { Id = 2, Teams = teams, SportId = football.Id, Date = date.AddDays(1) };
            encounterList.Add(encounter);
            encounterList.Add(encounter2);
            Encounter newEncounter = new Encounter() { Id = 3, Teams = teams, SportId = football.Id, Date = date.AddDays(2) };
            Assert.IsFalse(encounterBL.TeamsHaveEncountersOnTheSameDay(encounterList, newEncounter));
        }

        [TestMethod]
        public void CheckTeamsHaveEncountersOnTheSameDayOnAGivenListShouldReturnTrue()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol };
            ICollection<Team> teams2 = new List<Team>() { cerro, peñarol };
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            ICollection<Encounter> encounterList = new List<Encounter>();
            Encounter encounter = new Encounter() { Id = 1, Teams = teams, SportId = football.Id, Date = date };
            Encounter encounter2 = new Encounter() { Id = 2, Teams = teams, SportId = football.Id, Date = date.AddDays(1) };
            encounterList.Add(encounter);
            encounterList.Add(encounter2);
            Encounter encounterSameDate = new Encounter() { Id = 3, Teams = teams2, SportId = football.Id, Date = date };
            Assert.IsTrue(encounterBL.TeamsHaveEncountersOnTheSameDay(encounterList, encounterSameDate));
        }

        [TestMethod]
        public void GenerateFixtureFreeForAllTest()
        {
            var teamList = new List<Team>() { nacional, peñarol, defensor, danubio, cerro };
            List<Sport> sports = new List<Sport>() { football, basketball, footballWithTeams };
            mockSportRepo.Setup(r => r.Get(It.IsAny<Expression<Func<Sport, bool>>>(), null, "Teams")).Returns(sports);

            var generatedEncounters = encounterBL.GenerateFixture(DateTime.Now, footballWithTeams.Id, Algorithm.FreeForAll);
            var expectedEncountersCount = (teamList.Count() * (teamList.Count() - 1)) / 2;
            Assert.IsTrue(generatedEncounters.Count == expectedEncountersCount);
        }

        [TestMethod]
        public void GenerateFixtureRoundRobinTest()
        {
            var teamList = new List<Team>() { nacional, peñarol, defensor, danubio, cerro };
            List<Sport> sports = new List<Sport>() { footballWithTeams, football, basketball };
            mockSportRepo.Setup(r => r.Get(It.IsAny<Expression<Func<Sport, bool>>>(), null, "Teams")).Returns(sports);

            var generatedEncounters = encounterBL.GenerateFixture(DateTime.Now, footballWithTeams.Id, Algorithm.RoundRobin);
            var expectedEncountersCount = teamList.Count * (teamList.Count - 1);
            Assert.IsTrue(generatedEncounters.Count == expectedEncountersCount);
        }

        [TestMethod]
        [ExpectedException(typeof(FixtureGeneratorAlgorithmDoesNotExist))]
        public void GenerateFixtureShouldReturnExceptionTest()
        {
            var teamList = new List<Team>() { nacional, peñarol, defensor, danubio, cerro };
            mockSportRepo.Setup(r => r.Get(It.IsAny<Expression<Func<Sport, bool>>>(), null, "Teams")).Returns(new List<Sport>() { football });
            var generatedEncounters = encounterBL.GenerateFixture(DateTime.Now, 1, (Algorithm)2);
        }

        [TestMethod]
        public void CheckTeamsHaveEncountersOnTheSameDayOnAGivenListTeamsShouldReturnTrue()
        {
            ICollection<Team> teams = new List<Team>(){nacional, peñarol};
            ICollection<Team> teams2 = new List<Team>(){nacional, cerro};
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            ICollection<Encounter> encounterList = new List<Encounter>();
            Encounter encounter = new Encounter() { Id = 1, Teams = teams, SportId = football.Id, Date = date };
            Encounter encounter2 = new Encounter() { Id = 2, Teams = teams, SportId = football.Id, Date = date.AddDays(1) };
            encounterList.Add(encounter);
            encounterList.Add(encounter2);
            Encounter newEncounter = new Encounter() { Id = 3, Teams = teams2, SportId = 1, Date = date.AddDays(1) };
            Assert.IsTrue(encounterBL.TeamsHaveEncountersOnTheSameDay(encounterList, newEncounter));
        }

        [TestMethod]
        [ExpectedException(typeof(SportDoesNotSupportMultipleTeamsEncounters))]
        public void AddEncounterMultipleTeamsOnSportDoubleModeShouldReturnExceptionTest()
        {
            ICollection<Team> teams = new List<Team>() { nacional, peñarol, cerro };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };

            encounterBL.Add(encounter);
        }

    }
}