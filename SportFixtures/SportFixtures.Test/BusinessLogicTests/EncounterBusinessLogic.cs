using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportFixtures.Test.BusinessLogicTests
{
    [TestClass]
    public class EncounterBusinessLogicTests
    {
        private Context context;
        private IRepository<Encounter> repository;

        [TestMethod]
        public void AddEncounterOkTest()
        {
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1};
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1};
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            var encounterList = new List<Encounter>();
            var encounter = new Encounter() { Id = 1, Date = DateTime.Now, Sport = sport,Team1 = team1, Team2 = team2 };

            var mockEncounterRepo = new Mock<IRepository<Encounter>>();
            mockEncounterRepo.Setup(x => x.Insert(It.IsAny<Encounter>())).Callback<Encounter>(x => encounterList.Add(encounter));
            IEncounterBusinessLogic encounterBL = new EncounterBusinessLogic(mockEncounterRepo.Object);

            encounterBL.Add(encounter);
            mockEncounterRepo.Verify(x => x.Insert(It.IsAny<Encounter>()), Times.Once());
            mockEncounterRepo.Verify(x => x.Save(), Times.Once());
            Assert.IsTrue(encounterList.First().Id == encounter.Id);
        }
    }
}