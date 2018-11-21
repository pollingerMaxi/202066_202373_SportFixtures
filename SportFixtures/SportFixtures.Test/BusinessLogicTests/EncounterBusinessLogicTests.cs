using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.FixtureGenerator;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Enums;
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
        private EncountersTeams eNacional;
        private Team peñarol;
        private EncountersTeams ePeñarol;
        private Team cerro;
        private EncountersTeams eCerro;
        private Team defensor;
        private EncountersTeams eDefensor;
        private Team danubio;
        private EncountersTeams eDanubio;

        private Sport basketball;
        private Team aguada;
        private EncountersTeams eAguada;

        private Sport golf;
        private Team golfTeam1;
        private EncountersTeams eGolfTeam1;
        private Team golfTeam2;
        private EncountersTeams eGolfTeam2;
        private Team golfTeam3;
        private EncountersTeams eGolfTeam3;

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
            mockSportBL.Setup(s => s.GetById(2)).Returns(basketball);

            nacional = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            eNacional = new EncountersTeams() { Team = nacional, TeamId = nacional.Id };
            peñarol = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            ePeñarol = new EncountersTeams() { Team = peñarol, TeamId = peñarol.Id };
            cerro = new Team() { Id = 3, Name = "Cerro", SportId = 1 };
            eCerro = new EncountersTeams() { Team = cerro, TeamId = cerro.Id };
            defensor = new Team { Id = 4, Name = "Defensor", SportId = 1 };
            eDefensor = new EncountersTeams() { Team = defensor, TeamId = defensor.Id };
            danubio = new Team { Id = 5, Name = "Danubio", SportId = 1 };
            eDanubio = new EncountersTeams() { Team = danubio, TeamId = danubio.Id };


            var teamList = new List<Team>() { nacional, peñarol, defensor, danubio, cerro };
            footballWithTeams = new Sport() { Id = 3, Name = "Football", EncounterMode = EncounterMode.Double, Teams = teamList };
            mockSportBL.Setup(s => s.GetById(3)).Returns(footballWithTeams);

            aguada = new Team() { Id = 6, Name = "Aguada", SportId = 2 };
            eAguada = new EncountersTeams() { Team = aguada, TeamId = aguada.Id };

            golf = new Sport() { Id = 4, Name = "Golf", EncounterMode = EncounterMode.Multiple };
            mockSportBL.Setup(s => s.GetById(4)).Returns(golf);

            golfTeam1 = new Team() { Id = 7, Name = "golfTeam1", SportId = 4 };
            eGolfTeam1 = new EncountersTeams() { Team = golfTeam1, TeamId = golfTeam1.Id };
            golfTeam2 = new Team { Id = 8, Name = "golfTeam2", SportId = 4 };
            eGolfTeam2 = new EncountersTeams() { Team = golfTeam2, TeamId = golfTeam2.Id };
            golfTeam3 = new Team { Id = 9, Name = "golfTeam3", SportId = 4 };
            eGolfTeam3 = new EncountersTeams() { Team = golfTeam3, TeamId = golfTeam3.Id };
        }

        [TestMethod]
        public void AddEncounterOkTest()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));

            encounterBL.Add(encounter);
            mockEncounterRepo.Verify(x => x.Insert(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterSameTeamException))]
        public void AddEncounterShouldReturnSameTeamExceptionTest()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, eNacional };
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
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional};
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterTeamsDifferentSportException))]
        public void AddEncounterWithTeamsFromDifferentSportsShouldReturnExceptionTest()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, eAguada };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterSportDifferentFromTeamsSportException))]
        public void AddEncounterWithDifferentSportAsTeamsSportShouldReturnExceptionTest()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = basketball.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
        }

        [TestMethod]
        public void UpdateEncounterTeamOkTest()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            ICollection<EncountersTeams> updatedTeamList = new List<EncountersTeams>() { eCerro, ePeñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            encounterList.Add(encounter);
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(x => x.Update(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.First().Teams = updatedTeamList);
            mockEncounterRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(encounter);
            mockEncounterRepo.Setup(e => e.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, "Teams")).Returns(encounterList);
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
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            DateTime updatedDate = new DateTime(2018, 9, 27, 8, 30, 00);
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            encounterList.Add(encounter);
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(x => x.Update(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.First().Date = updatedDate);
            mockEncounterRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(encounter);
            mockEncounterRepo.Setup(e => e.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, "Teams")).Returns(encounterList);
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
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            ICollection<EncountersTeams> teamsDifferentSports = new List<EncountersTeams>() { eNacional, eAguada };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            encounterList.Add(encounter);
            encounterList.Add(encounter);
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(encounter);
            mockEncounterRepo.Setup(e => e.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, "Teams")).Returns(encounterList);
            encounterBL.Add(encounter);
            encounter.Teams = teamsDifferentSports;
            encounterBL.Update(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterSportDifferentFromTeamsSportException))]
        public void UpdateEncounterSportWithDifferentTeamSportsShouldReturnsExceptionTest()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(encounter);
            mockEncounterRepo.Setup(e => e.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, "Teams")).Returns(encounterList);
            encounterBL.Add(encounter);
            encounter.SportId = basketball.Id;
            encounterBL.Update(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterDoesNotExistException))]
        public void UpdateEncounterThatDoesntExistsReturnsExceptionTest()
        {
            DateTime date = DateTime.Now;
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            var encounter = new Encounter() { Id = 1, Date = date, SportId = football.Id, Teams = teams };
            var notAddedEncounter = new Encounter() { Id = 2, Date = date.AddDays(1), SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
            encounterBL.Update(notAddedEncounter);
        }

        [TestMethod]
        public void DeleteEncounterOkTest()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            encounterList.Add(encounter);
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(x => x.Delete(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.RemoveAt(0));
            mockEncounterRepo.Setup(e => e.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, "Teams")).Returns(encounterList);
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
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
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
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
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
            ICollection<EncountersTeams> encounterTeams = new List<EncountersTeams>() { eNacional, ePeñarol };
            ICollection<EncountersTeams> encounter2Teams = new List<EncountersTeams>() { eCerro, ePeñarol };
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
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            encounterList.Add(encounter);
            int sportId = 1;
            encounterBL.GetAllEncountersOfSport(sportId);
            mockEncounterRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, ""), Times.Once());
        }

        [TestMethod]
        public void GetAllEncountersOfTeamTest()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            encounterList.Add(encounter);
            encounterBL.GetAllEncountersOfTeam(nacional.Id);
            mockEncounterRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, ""), Times.Once());
        }

        [TestMethod]
        public void GetAllEncountersOfTheDay()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
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
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };
            encounterList.Add(encounter);
            mockEncounterRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(encounter);
            mockEncounterRepo.Setup(e => e.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, "Teams")).Returns(encounterList);
            var encounterFromDb = encounterBL.GetById(It.IsAny<int>());
            mockEncounterRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, "Teams"), Times.Once);
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
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
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
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            ICollection<EncountersTeams> teams2 = new List<EncountersTeams>() { eCerro, ePeñarol };
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
        public void CheckTeamsHaveEncountersOnTheSameDayOnAGivenListTeamsShouldReturnTrue()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            ICollection<EncountersTeams> teams2 = new List<EncountersTeams>() { eNacional, eCerro };
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
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol, eCerro };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = football.Id, Teams = teams };

            encounterBL.Add(encounter);
        }

        [TestMethod]
        public void AddEncounterMultipleTeamsOkTest()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eGolfTeam1, eGolfTeam2, eGolfTeam3 };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = golf.Id, Teams = teams };

            encounterBL.Add(encounter);
        }

        [TestMethod]
        public void AddEncounterWithResultsOk()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol};
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            PositionInEncounter scoreNacional = new PositionInEncounter() { TeamId = nacional.Id, Position = 3 };
            PositionInEncounter scorePeñarol = new PositionInEncounter() { TeamId = peñarol.Id, Position = 0 };
            ICollection<PositionInEncounter> results = new List<PositionInEncounter>() { scoreNacional, scorePeñarol };
            ICollection<Encounter> encounterList = new List<Encounter>();
            Encounter encounter = new Encounter() { Id = 1, Teams = teams, SportId = football.Id, Date = date, Results = results };
            encounterList.Add(encounter);
            encounterBL.Add(encounter);
            mockEncounterRepo.Verify(x => x.Insert(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Save(), Times.Exactly(1));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidResultsForEncounterException))]
        public void AddScoreWithDifferentTeamsThanEncounterShouldReturnExceptionTest()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eCerro, eDanubio };
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            PositionInEncounter scoreNacional = new PositionInEncounter() { TeamId = nacional.Id, Position = 3 };
            PositionInEncounter scorePeñarol = new PositionInEncounter() { TeamId = peñarol.Id, Position = 0 };
            ICollection<PositionInEncounter> results = new List<PositionInEncounter>() { scoreNacional, scorePeñarol };
            ICollection<Encounter> encounterList = new List<Encounter>();
            Encounter encounter = new Encounter() { Id = 1, Teams = teams, SportId = football.Id, Date = date, Results = results };
            encounterList.Add(encounter);
            encounterBL.Add(encounter);
        }

        [TestMethod]
        public void AddResultsToEncounterOkTest()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            PositionInEncounter scoreNacional = new PositionInEncounter() { TeamId = nacional.Id, Position = 3 };
            PositionInEncounter scorePeñarol = new PositionInEncounter() { TeamId = peñarol.Id, Position = 0 };
            ICollection<PositionInEncounter> results = new List<PositionInEncounter>() { scoreNacional, scorePeñarol };
            ICollection<Encounter> encounterList = new List<Encounter>();
            Encounter encounter = new Encounter() { Id = 1, Teams = teams, SportId = football.Id, Date = date };
            encounterList.Add(encounter);
            mockEncounterRepo.Setup(e => e.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, "Teams")).Returns(encounterList);
            encounterBL.Add(encounter);
            encounterBL.AddResults(results, encounter.Id);
            mockEncounterRepo.Verify(x => x.Insert(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Save(), Times.Exactly(2));
            mockEncounterRepo.Verify(x => x.Update(encounter), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidResultsForEncounterException))]
        public void AddResultsToEncounterShouldReturnExceptionTest()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            PositionInEncounter scoreDanubio = new PositionInEncounter() { TeamId = danubio.Id, Position = 3 };
            PositionInEncounter scoreCerro = new PositionInEncounter() { TeamId = cerro.Id, Position = 0 };
            ICollection<PositionInEncounter> results = new List<PositionInEncounter>() { scoreDanubio, scoreCerro };
            ICollection<Encounter> encounterList = new List<Encounter>();
            Encounter encounter = new Encounter() { Id = 1, Teams = teams, SportId = football.Id, Date = date };
            encounterList.Add(encounter);
            mockEncounterRepo.Setup(e => e.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, "Teams")).Returns(encounterList);
            encounterBL.Add(encounter);
            mockEncounterRepo.Verify(x => x.Insert(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Save(), Times.Exactly(1));
            encounterBL.AddResults(results, encounter.Id);
        }

        [TestMethod]
        public void AddManyEncountersOkTest()
        {
            ICollection<EncountersTeams> teams = new List<EncountersTeams>() { eNacional, ePeñarol };
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            var encounter = new Encounter() { Id = 1, Date = date, SportId = football.Id, Teams = teams };
            var encounter2 = new Encounter() { Id = 2, Date = date.AddDays(5), SportId = football.Id, Teams = teams };
            mockEncounterRepo.Setup(x => x.Insert(encounter)).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(x => x.Insert(encounter2)).Callback<Encounter>(x => encounterList.Add(encounter2));
            var encounters = new List<Encounter>(){encounter, encounter2};
            encounterBL.AddMany(encounters);
            mockEncounterRepo.Verify(x => x.Insert(It.IsAny<Encounter>()), Times.Exactly(2));
            mockEncounterRepo.Verify(x => x.Save(), Times.Once());
        }

    }
}