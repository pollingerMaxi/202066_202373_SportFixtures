using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.PositionExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace SportFixtures.Test.BusinessLogicTests
{
    [TestClass]
    public class PositionBusinessLogicTests
    {
        private const dynamic NO_BUSINESS_LOGIC = null;
        private const dynamic NO_UT_REPOSITORY = null;
        private Mock<IRepository<Position>> mockPositionRepo;
        private Mock<IRepository<Encounter>> mockEncounterRepo;
        private Mock<IRepository<Sport>> mockSportRepo;
        private IPositionBusinessLogic positionBL;
        private IEncounterBusinessLogic encounterBL;
        private Mock<ISportBusinessLogic> mockSportBL;
        private Mock<IEncounterBusinessLogic> mockEncounterBL;
        private Encounter encounter;
        private List<Encounter> encounterList;
        private Team nacional;
        private Team peñarol;
        private Sport futbol;
        private List<Position> positionList;

        [TestInitialize]
        public void TestInitialize()
        {
            mockEncounterBL = new Mock<IEncounterBusinessLogic>();
            mockSportBL = new Mock<ISportBusinessLogic>();
            mockPositionRepo = new Mock<IRepository<Position>>();
            positionBL = new PositionBusinessLogic(mockPositionRepo.Object, mockSportBL.Object, mockEncounterBL.Object);
            
            nacional = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            peñarol = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            ICollection<Team> teams = new List<Team>(){nacional, peñarol};
            futbol = new Sport() { Id = 1, Name = "Futbol", Teams = teams};
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Teams = teams };
            encounterList = new List<Encounter>() { encounter };
            positionList = new List<Position>();
        }

        [TestMethod]
        public void AddPositionOkTest()
        {
            Position position = new Position() {Id = 1, Team = nacional, SportId = futbol.Id, Points = 1};
            mockPositionRepo.Setup(x => x.Insert(It.IsAny<Position>())).Callback<Position>(x => positionList.Add(position));
            mockPositionRepo.Setup(e => e.Get(It.IsAny<Expression<Func<Position, bool>>>(), null, "Teams")).Returns(positionList);
            positionBL.Add(position);
            mockPositionRepo.Verify(x => x.Insert(It.IsAny<Position>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(TeamPositionAlreadyExistsExeption))]
        public void AddPositionShouldReturnExceptionTest()
        {
            Position position = new Position() {Id = 1, Team = nacional, SportId = futbol.Id, Points = 1};
            mockPositionRepo.Setup(x => x.Insert(It.IsAny<Position>())).Callback<Position>(x => positionList.Add(position));
            mockPositionRepo.Setup(e => e.Get(It.IsAny<Expression<Func<Position, bool>>>(), null, "Teams")).Returns(positionList);
            positionBL.Add(position);
            Position position2 = new Position() {Id = 2, Team = nacional, SportId = futbol.Id, Points = 1};
            positionBL.Add(position2);
        }
    }
}