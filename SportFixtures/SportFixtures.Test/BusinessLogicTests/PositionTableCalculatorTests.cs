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
        private Team peñarol;
        private Team cerro;
        private Team defensor;
        private Team danubio;

        private Sport golf;
        private Team golfTeam1;
        private Team golfTeam2;
        private Team golfTeam3;

        [TestInitialize]
        public void TestInitialize()
        {
            mockSportBL = new Mock<ISportBusinessLogic>();
            mockEncounterBL = new Mock<IEncounterBusinessLogic>();
            positionTableCalculator = new PositionTableCalculator(mockSportBL.Object, mockEncounterBL.Object);
            encounterList = new List<Encounter>();

            football = new Sport() { Id = 1, Name = "Football", EncounterMode = EncounterMode.Double };
            

            nacional = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            peñarol = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            cerro = new Team() { Id = 3, Name = "Cerro", SportId = 1 };
            defensor = new Team { Id = 4, Name = "Defensor", SportId = 1 };
            danubio = new Team { Id = 5, Name = "Danubio", SportId = 1 };

            football.Teams = new List<Team>(){nacional, peñarol, cerro, danubio, defensor};

            

            golf = new Sport() { Id = 2, Name = "Golf", EncounterMode = EncounterMode.Multiple };
            mockSportBL.Setup(s => s.GetById(4)).Returns(golf); 

            golfTeam1 = new Team() { Id = 7, Name = "golfTeam1", SportId = 4 };
            golfTeam2 = new Team { Id = 8, Name = "golfTeam2", SportId = 4 };
            golfTeam3 = new Team { Id = 9, Name = "golfTeam3", SportId = 4 };

            golf.Teams = new List<Team>(){golfTeam1, golfTeam2, golfTeam3};

            mockSportBL.Setup(s => s.GetById(1)).Returns(football);
            mockSportBL.Setup(s => s.GetById(2)).Returns(golf);
        }

        [TestMethod]
        public void GeneratePositionTableForDoubleEncounterMode(){
            Score scoreNacional = new Score {Id = 1, Position = 0, TeamId = nacional.Id};
            Score scoreCerro = new Score {Id = 2, Position = 2, TeamId = cerro.Id};
            Encounter encounter = new Encounter(){Id = 1, Teams = {nacional, cerro}, Results = {scoreNacional, scoreCerro}};
            encounterList.Add(encounter);
            mockEncounterBL.Setup(s => s.GetAllEncountersOfTeam(nacional.Id)).Returns(encounterList);
            mockEncounterBL.Setup(s => s.GetAllEncountersOfTeam(cerro.Id)).Returns(encounterList);
            ICollection<Position> positionTable = positionTableCalculator.GeneratePositionTable(football.Id);
            Assert.IsTrue(positionTable.Count() == 2);
        }

    }
}