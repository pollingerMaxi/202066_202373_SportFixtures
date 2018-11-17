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
    public class PositionTableCalculatorTests
    {
        private const dynamic NO_BUSINESS_LOGIC = null;
        private IPositionTableCalculator positionTableCalculator;
        private Mock<ISportBusinessLogic> mockSportBL;
        private Mock<IEncounterBusinessLogic> mockEncounterBL;
        private List<Encounter> encounterList;
        private Sport football;
        private Team nacional;
        private EncountersTeams eNacional;
        private Team peñarol;
        private EncountersTeams ePeñarol;
        private Team cerro;
        private EncountersTeams eCerro;
        private Team defensor;
        private Team danubio;

        private Sport golf;
        private Team golfTeam1;
        private EncountersTeams eGolfTeam1;
        private Team golfTeam2;
        private EncountersTeams eGolfTeam2;
        private Team golfTeam3;
        private EncountersTeams eGolfTeam3;
        private Team golfTeam4;
        private EncountersTeams eGolfTeam4;

        [TestInitialize]
        public void TestInitialize()
        {
            mockSportBL = new Mock<ISportBusinessLogic>();
            mockEncounterBL = new Mock<IEncounterBusinessLogic>();
            positionTableCalculator = new PositionTableCalculator(mockSportBL.Object, mockEncounterBL.Object);
            encounterList = new List<Encounter>();

            football = new Sport() { Id = 1, Name = "Football", EncounterMode = EncounterMode.Double };

            nacional = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            eNacional = new EncountersTeams() { Team = nacional, TeamId = nacional.Id };
            peñarol = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            ePeñarol = new EncountersTeams() { Team = peñarol, TeamId = peñarol.Id };
            cerro = new Team() { Id = 3, Name = "Cerro", SportId = 1 };
            eCerro = new EncountersTeams() { Team = cerro, TeamId = cerro.Id };
            defensor = new Team { Id = 4, Name = "Defensor", SportId = 1 };
            danubio = new Team { Id = 5, Name = "Danubio", SportId = 1 };

            football.Teams = new List<Team>() { nacional, cerro };



            golf = new Sport() { Id = 2, Name = "Golf", EncounterMode = EncounterMode.Multiple };
            mockSportBL.Setup(s => s.GetById(4)).Returns(golf);

            golfTeam1 = new Team() { Id = 7, Name = "golfTeam1", SportId = 4 };
            eGolfTeam1 = new EncountersTeams() { Team = golfTeam1, TeamId = golfTeam1.Id };
            golfTeam2 = new Team { Id = 8, Name = "golfTeam2", SportId = 4 };
            eGolfTeam2 = new EncountersTeams() { Team = golfTeam2, TeamId = golfTeam2.Id };
            golfTeam3 = new Team { Id = 9, Name = "golfTeam3", SportId = 4 };
            eGolfTeam3 = new EncountersTeams() { Team = golfTeam3, TeamId = golfTeam3.Id };
            golfTeam4 = new Team { Id = 10, Name = "golfTeam4", SportId = 4 };
            eGolfTeam4 = new EncountersTeams() { Team = golfTeam4, TeamId = golfTeam4.Id };

            golf.Teams = new List<Team>() { golfTeam1, golfTeam2, golfTeam3, golfTeam4 };

            mockSportBL.Setup(s => s.GetById(1)).Returns(football);
            mockSportBL.Setup(s => s.GetById(2)).Returns(golf);
        }

        [TestMethod]
        public void GeneratePositionTableForDoubleEncounterModeTieTest()
        {
            PositionInEncounter scoreNacional = new PositionInEncounter { Id = 1, Position = 1, TeamId = nacional.Id };
            PositionInEncounter scoreCerro = new PositionInEncounter { Id = 2, Position = 1, TeamId = cerro.Id };
            Encounter encounter = new Encounter() { Id = 1, Teams = { eNacional, eCerro }, Results = { scoreNacional, scoreCerro } };
            encounterList.Add(encounter);
            mockEncounterBL.Setup(s => s.GetAllEncountersOfTeam(nacional.Id)).Returns(encounterList);
            mockEncounterBL.Setup(s => s.GetAllEncountersOfTeam(cerro.Id)).Returns(encounterList);
            ICollection<Score> positionTable = positionTableCalculator.GeneratePositionTableForSport(football.Id);
            Assert.IsTrue(positionTable.Count() == 2);
        }

        [TestMethod]
        public void GeneratePositionTableForDoubleEncounterModeTest()
        {
            PositionInEncounter scoreNacional = new PositionInEncounter { Id = 1, Position = 0, TeamId = nacional.Id };
            PositionInEncounter scoreCerro = new PositionInEncounter { Id = 2, Position = 2, TeamId = cerro.Id };
            Encounter encounter = new Encounter() { Id = 1, Teams = { eNacional, eCerro }, Results = { scoreNacional, scoreCerro } };
            encounterList.Add(encounter);
            mockEncounterBL.Setup(s => s.GetAllEncountersOfTeam(nacional.Id)).Returns(encounterList);
            mockEncounterBL.Setup(s => s.GetAllEncountersOfTeam(cerro.Id)).Returns(encounterList);
            ICollection<Score> positionTable = positionTableCalculator.GeneratePositionTableForSport(football.Id);
            Assert.IsTrue(positionTable.Count() == 2);
        }

        [TestMethod]
        public void GeneratePositionTableForMultipleEncounterModeTest()
        {
            PositionInEncounter scoreGolfTeam1 = new PositionInEncounter { Id = 1, Position = 1, TeamId = golfTeam1.Id };
            PositionInEncounter scoreGolfTeam2 = new PositionInEncounter { Id = 2, Position = 2, TeamId = golfTeam2.Id };
            PositionInEncounter scoreGolfTeam3 = new PositionInEncounter { Id = 3, Position = 3, TeamId = golfTeam3.Id };
            PositionInEncounter scoreGolfTeam4 = new PositionInEncounter { Id = 4, Position = 4, TeamId = golfTeam4.Id };
            Encounter encounter = new Encounter() { Id = 1, Teams = { eGolfTeam1, eGolfTeam2, eGolfTeam3, eGolfTeam4 }, Results = { scoreGolfTeam1, scoreGolfTeam2, scoreGolfTeam3, scoreGolfTeam4 } };
            encounterList.Add(encounter);
            mockEncounterBL.Setup(s => s.GetAllEncountersOfTeam(It.IsAny<int>())).Returns(encounterList);
            ICollection<Score> positionTable = positionTableCalculator.GeneratePositionTableForSport(golf.Id);
            Assert.IsTrue(positionTable.Count() == 4);
        }

    }
}