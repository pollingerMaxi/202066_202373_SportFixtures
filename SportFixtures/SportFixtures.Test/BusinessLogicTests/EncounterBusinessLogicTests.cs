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

namespace SportFixtures.Test.BusinessLogicTests
{
    [TestClass]
    public class EncounterBusinessLogicTests
    {
        private const dynamic NO_BUSINESS_LOGIC = null;
        private Mock<IRepository<Encounter>> mockEncounterRepo;
        private Mock<IRepository<Sport>> mockSportRepo;
        private IEncounterBusinessLogic encounterBL;
        private ISportBusinessLogic sportBL;
        private List<Encounter> encounterList;

        [TestInitialize]
        public void TestInitialize()
        {
            mockEncounterRepo = new Mock<IRepository<Encounter>>();
            mockSportRepo = new Mock<IRepository<Sport>>();
            sportBL = new SportBusinessLogic(mockSportRepo.Object);
            encounterBL = new EncounterBusinessLogic(mockEncounterRepo.Object, sportBL);
            encounterList = new List<Encounter>();
            mockEncounterRepo.Setup(r => r.Get(null, null, "")).Returns(encounterList);
            mockEncounterRepo.Setup(r => r.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, "")).Returns(encounterList);
        }

        [TestMethod]
        public void AddEncounterOkTest()
        {
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team1, Team2 = team2 };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
            mockEncounterRepo.Verify(x => x.Insert(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterSameTeamException))]
        public void AddEncounterShouldReturnSameTeamExceptionTest()
        {
            var team = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team, Team2 = team };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterTeamsCantBeNullException))]
        public void AddEncounterWithoutTeamsShouldReturnNullTeamsExceptionTest()
        {
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterTeamsCantBeNullException))]
        public void AddEncounterWithoutOneTeamShouldReturnNullTeamsExceptionTest()
        {
            var team = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterTeamsDifferentSportException))]
        public void AddEncounterWithTeamsFromDifferentSportsShouldReturnExceptionTest()
        {
            var team = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 1, Name = "Aguada", SportId = 2 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team, Team2 = team2 };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterSportDifferentFromTeamsSportException))]
        public void AddEncounterWithDifferentSportAsTeamsSportShouldReturnExceptionTest()
        {
            var team = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 1, Name = "Peñarol", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var sport2 = new Sport() { Id = 2, Name = "Basquetbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport2.Id, Team1 = team, Team2 = team2 };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
        }

        [TestMethod]
        public void UpdateEncounterTeamOkTest()
        {
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var updatedTeam = new Team() { Id = 3, Name = "River", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team1, Team2 = team2 };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(x => x.Update(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.First().Team1 = updatedTeam);
            mockEncounterRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(encounter);
            encounterBL.Add(encounter);
            encounter.Team1 = updatedTeam;
            encounterBL.Update(encounter);
            mockEncounterRepo.Verify(x => x.Insert(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Update(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Save(), Times.AtLeast(2));
        }

        [TestMethod]
        public void UpdateEncounterDateOkTest()
        {
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            DateTime updatedDate = new DateTime(2018, 9, 27, 8, 30, 00);
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team1, Team2 = team2 };
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
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var updatedTeam = new Team() { Id = 3, Name = "River", SportId = 2 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team1, Team2 = team2 };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(x => x.Update(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.First().Team1 = updatedTeam);
            mockEncounterRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(encounter);
            encounterBL.Add(encounter);
            encounter.Team1 = updatedTeam;
            encounterBL.Update(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterSportDifferentFromTeamsSportException))]
        public void UpdateEncounterSportWithDifferentTeamSportsShouldReturnsExceptionTest()
        {
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var updatedSport = new Sport() { Id = 2, Name = "Basquetbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team1, Team2 = team2 };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(x => x.Update(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.First().SportId = updatedSport.Id);
            mockEncounterRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(encounter);
            encounterBL.Add(encounter);
            encounter.SportId = updatedSport.Id;
            encounterBL.Update(encounter);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterDoesNotExistException))]
        public void UpdateEncounterThatDoesntExistsReturnsExceptionTest()
        {
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team1, Team2 = team2 };
            var notAddedEncounter = new Encounter() { Id = 2, Date = DateTime.Now, SportId = sport.Id, Team1 = team1, Team2 = team2 };
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            encounterBL.Add(encounter);
            encounterBL.Update(notAddedEncounter);
        }

        [TestMethod]
        public void DeleteEncounterOkTest()
        {
            var team = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 1, Name = "River", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team, Team2 = team2 };
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
            var team = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 1, Name = "River", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team, Team2 = team2 };
            encounterBL.Delete(encounter.Id);
        }

        [TestMethod]
        public void CheckIfTeamsHaveAnEncounterOnTheSameDateOkTest()
        {
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            DateTime date2 = new DateTime(2018, 9, 28, 8, 30, 00);
            var encounter = new Encounter() { Id = 1, Date = date, SportId = sport.Id, Team1 = team1, Team2 = team2 };
            var encounter2 = new Encounter() { Id = 2, Date = date2, SportId = sport.Id, Team1 = team1, Team2 = team2 };
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
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            var encounter = new Encounter() { Id = 1, Date = date, SportId = sport.Id, Team1 = team1, Team2 = team2 };
            var encounter2 = new Encounter() { Id = 2, Date = date, SportId = sport.Id, Team1 = team2, Team2 = team1 };
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
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var team3 = new Team() { Id = 3, Name = "Cerro", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            DateTime date2 = new DateTime(2018, 9, 28, 8, 30, 00);
            var encounter = new Encounter() { Id = 1, Date = date, SportId = sport.Id, Team1 = team1, Team2 = team2 };
            var encounter2 = new Encounter() { Id = 2, Date = date2, SportId = sport.Id, Team1 = team1, Team2 = team2 };
            var encounter3 = new Encounter() { Id = 3, Date = date2, SportId = sport.Id, Team1 = team1, Team2 = team3 };
            mockEncounterRepo.Setup(x => x.Insert(encounter)).Callback<Encounter>(x => encounterList.Add(encounter));
            mockEncounterRepo.Setup(x => x.Insert(encounter2)).Callback<Encounter>(x => encounterList.Add(encounter2));
            encounterBL.Add(encounter);
            encounterBL.Add(encounter2);
            encounterBL.Add(encounter3);
        }

        [TestMethod]
        [ExpectedException(typeof(TeamAlreadyHasAnEncounterOnTheSameDayException))]
        public void CheckTeamHasEncounterSameDateShouldReturnException2Test()
        {
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var team3 = new Team() { Id = 3, Name = "Cerro", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            DateTime date2 = new DateTime(2018, 9, 28, 8, 30, 00);
            var encounter = new Encounter() { Id = 1, Date = date, SportId = sport.Id, Team1 = team1, Team2 = team2 };
            var encounter2 = new Encounter() { Id = 2, Date = date2, SportId = sport.Id, Team1 = team1, Team2 = team2 };
            var encounter3 = new Encounter() { Id = 3, Date = date2, SportId = sport.Id, Team1 = team3, Team2 = team1 };
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
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team1, Team2 = team2 };
            encounterList.Add(encounter);
            int sportId = 1;
            encounterBL.GetAllEncountersOfSport(sportId);
            mockEncounterRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, ""), Times.Once());
        }

        [TestMethod]
        public void GetAllEncountersOfTeamTest()
        {
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team1, Team2 = team2 };
            encounterList.Add(encounter);
            int teamId = 1;
            encounterBL.GetAllEncountersOfTeam(teamId);
            mockEncounterRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Encounter, bool>>>(), null, ""), Times.Once());
        }

        [TestMethod]
        public void GetAllEncountersOfTheDay()
        {
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team1, Team2 = team2 };
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
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team1, Team2 = team2 };
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
        public void CheckTeamsHaveEncountersOnTheSameDayOnAGivenListShouldReturnFalse(){
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            ICollection<Encounter> encounterList = new List<Encounter>();
            Encounter encounter = new Encounter(){Id = 1, Team1 = team1, Team2 = team2, SportId = 1, Date = date};
            Encounter encounter2 = new Encounter(){Id = 2, Team1 = team1, Team2 = team2, SportId = 1, Date = date.AddDays(1)};
            encounterList.Add(encounter);
            encounterList.Add(encounter2);
            Encounter newEncounter = new Encounter(){Id = 3, Team1 = team1, Team2 = team2, SportId = 1, Date = date.AddDays(2)};
            Assert.IsFalse(encounterBL.TeamsHaveEncountersOnTheSameDay(encounterList, newEncounter));
        }

        [TestMethod]
        public void CheckTeamsHaveEncountersOnTheSameDayOnAGivenListShouldReturnTrue(){
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            ICollection<Encounter> encounterList = new List<Encounter>();
            Encounter encounter = new Encounter(){Id = 1, Team1 = team1, Team2 = team2, SportId = 1, Date = date};
            Encounter encounter2 = new Encounter(){Id = 2, Team1 = team1, Team2 = team2, SportId = 1, Date = date.AddDays(1)};
            encounterList.Add(encounter);
            encounterList.Add(encounter2);

            Assert.IsTrue(encounterBL.TeamsHaveEncountersOnTheSameDay(encounterList, encounter));
        }

        [TestMethod]
        public void GenerateFixtureFreeForAllTest(){

            Team nacional = new Team { Id = 1, Name = "Nacional", SportId = 1 };
            Team peñarol = new Team { Id = 2, Name = "Peñarol", SportId = 1 };
            Team defensor = new Team { Id = 3, Name = "Defensor", SportId = 1 };
            Team danubio = new Team { Id = 4, Name = "Danubio", SportId = 1 };
            Team cerro = new Team { Id = 5, Name = "Cerro", SportId = 1 };
            var teamList = new List<Team>(){nacional, peñarol, defensor, danubio, cerro};
            Sport sport = new Sport(){Id = 1, Name = "Futbol", Teams = teamList};
            
            mockSportRepo.Setup(r => r.GetById(1)).Returns(sport);
            
            var generatedEncounters = encounterBL.GenerateFixture(DateTime.Now, 1, Algorithm.FreeForAll);
            var expectedEncountersCount = (teamList.Count() * (teamList.Count() - 1)) / 2;
            Assert.IsTrue(generatedEncounters.Count == expectedEncountersCount);
        }

        [TestMethod]
        public void GenerateFixtureRoundRobinTest(){

            Team nacional = new Team { Id = 1, Name = "Nacional", SportId = 1 };
            Team peñarol = new Team { Id = 2, Name = "Peñarol", SportId = 1 };
            Team defensor = new Team { Id = 3, Name = "Defensor", SportId = 1 };
            Team danubio = new Team { Id = 4, Name = "Danubio", SportId = 1 };
            Team cerro = new Team { Id = 5, Name = "Cerro", SportId = 1 };
            var teamList = new List<Team>(){nacional, peñarol, defensor, danubio, cerro};
            Sport sport = new Sport(){Id = 1, Name = "Futbol", Teams = teamList};
            
            mockSportRepo.Setup(r => r.GetById(1)).Returns(sport);
            
            var generatedEncounters = encounterBL.GenerateFixture(DateTime.Now, 1, Algorithm.RoundRobin);
            var expectedEncountersCount = teamList.Count * (teamList.Count - 1);
            Assert.IsTrue(generatedEncounters.Count == expectedEncountersCount);
        }

        // [TestMethod]
        // public void GenerateFixtureShouldReturnExceptionTest(){

        //     Team nacional = new Team { Id = 1, Name = "Nacional", SportId = 1 };
        //     Team peñarol = new Team { Id = 2, Name = "Peñarol", SportId = 1 };
        //     Team defensor = new Team { Id = 3, Name = "Defensor", SportId = 1 };
        //     Team danubio = new Team { Id = 4, Name = "Danubio", SportId = 1 };
        //     Team cerro = new Team { Id = 5, Name = "Cerro", SportId = 1 };
        //     var teamList = new List<Team>(){nacional, peñarol, defensor, danubio, cerro};
        //     Sport sport = new Sport(){Id = 1, Name = "Futbol", Teams = teamList};
            
        //     mockSportRepo.Setup(r => r.GetById(1)).Returns(sport);
        
        //     var generatedEncounters = encounterBL.GenerateFixture(DateTime.Now, 1);
        //     var expectedEncountersCount = teamList.Count * (teamList.Count - 1);
        //     Assert.IsTrue(generatedEncounters.Count == expectedEncountersCount);
        // }
        public void CheckTeamsHaveEncountersOnTheSameDayOnAGivenListTeamsShouldReturnTrue2(){
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var team3 = new Team() { Id = 3, Name = "Danubio", SportId = 1 };
            DateTime date = new DateTime(2018, 9, 27, 8, 30, 00);
            ICollection<Encounter> encounterList = new List<Encounter>();
            Encounter encounter = new Encounter(){Id = 1, Team1 = team1, Team2 = team2, SportId = 1, Date = date};
            Encounter encounter2 = new Encounter(){Id = 2, Team1 = team1, Team2 = team2, SportId = 1, Date = date.AddDays(1)};
            encounterList.Add(encounter);
            encounterList.Add(encounter2);
            Encounter newEncounter = new Encounter(){Id = 3, Team1 = team3, Team2 = team1, SportId = 1, Date = date.AddDays(1)};
            Assert.IsTrue(encounterBL.TeamsHaveEncountersOnTheSameDay(encounterList, newEncounter));
        }
    }
}