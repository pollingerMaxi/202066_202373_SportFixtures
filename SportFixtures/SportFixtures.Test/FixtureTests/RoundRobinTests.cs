using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.EncounterExceptions;
using SportFixtures.FixtureGenerator;
using SportFixtures.FixtureGenerator.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Test.FixtureTests
{
    [TestClass]
    public class RoundRobinTests
    {
        private Context context;
        private IEncounterBusinessLogic encounterBL;
        private IRepository<Encounter> encounterRepository;
        private IRepository<Team> teamRepository;
        private IFixtureGenerator roundRobin;
        private List<Team> teamList;
        private Team nacional;
        private Team peñarol;
        private Team defensor;
        private Team danubio;
        private Team cerro;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "roundRobinDB").Options;
            context = new Context(options);
            encounterRepository = new GenericRepository<Encounter>(context);
            encounterBL = new EncounterBusinessLogic(encounterRepository, null);
            teamRepository = new GenericRepository<Team>(context);

            nacional = new Team { Id = 1, Name = "Nacional", SportId = 1 };
            peñarol = new Team { Id = 2, Name = "Peñarol", SportId = 1 };
            defensor = new Team { Id = 3, Name = "Defensor", SportId = 1 };
            danubio = new Team { Id = 4, Name = "Danubio", SportId = 1 };
            cerro = new Team { Id = 5, Name = "Cerro", SportId = 1 };

            teamList = new List<Team>() { nacional, peñarol, defensor, danubio, cerro };

            roundRobin = new RoundRobin(encounterBL);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            context.RemoveRange(encounterRepository.Get(null, null, ""));
            context.SaveChanges();
        }

        [TestMethod]
        public void GenerateRoundRobinWithFiveTeamsTest()
        {
            var encounters = roundRobin.GenerateFixture(teamList, DateTime.Now);
            var NtimesNminus1 = teamList.Count * (teamList.Count - 1);
            Assert.IsTrue(encounters.Count == NtimesNminus1);
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughTeamsForEncounterException))]
        public void GenerateRoundRobinWithZeroTeamsTest()
        {
            teamList.Clear();
            var encounters = roundRobin.GenerateFixture(teamList, DateTime.Now);
            var expectedGeneratedEncountersCount = teamList.Count * (teamList.Count - 1);
            Assert.IsTrue(encounters.Count == expectedGeneratedEncountersCount);
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughTeamsForEncounterException))]
        public void GenerateRoundRobinWithOneTeamTest()
        {
            teamList.Clear();
            teamList.Add(nacional);
            var encounters = roundRobin.GenerateFixture(teamList, DateTime.Now);
            var expectedGeneratedEncountersCount = teamList.Count * (teamList.Count - 1);
            Assert.IsTrue(encounters.Count == expectedGeneratedEncountersCount);
        }

        [TestMethod]
        public void GenerateRoundRobinWithTwoTeamsTest()
        {
            teamList.Clear();
            teamList.Add(nacional);
            teamList.Add(peñarol);
            var encounters = roundRobin.GenerateFixture(teamList, DateTime.Now);
            var expectedGeneratedEncountersCount = teamList.Count * (teamList.Count - 1);
            Assert.IsTrue(encounters.Count == expectedGeneratedEncountersCount);
        }
    }
}
