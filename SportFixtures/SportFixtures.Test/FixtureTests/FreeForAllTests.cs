using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using SportFixtures.FixtureGenerator;
using SportFixtures.FixtureGenerator.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SportFixtures.Exceptions.EncounterExceptions;
using SportFixtures.Data;

namespace SportFixtures.Test.FixtureTests
{
    [TestClass]
    public class FreeForAllTests
    {
        private Context context;
        private IEncounterBusinessLogic encounterBL;
        private IRepository<Encounter> encounterRepository;
        private IRepository<Team> teamRepository;
        private IFixtureGenerator freeForAll;
        private Mock<IRepository<Sport>> mockSportRepo;
        private Mock<ISportBusinessLogic> mockSportBL;
        private List<Team> teamList;
        private Sport football;
        private Team nacional;
        private Team peñarol;
        private Team defensor;
        private Team danubio;
        private Team cerro;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "encounterDB").Options;
            context = new Context(options);
            mockSportRepo = new Mock<IRepository<Sport>>();
            mockSportBL = new Mock<ISportBusinessLogic>();
            encounterRepository = new GenericRepository<Encounter>(context);
            encounterBL = new EncounterBusinessLogic(encounterRepository, mockSportBL.Object);
            teamRepository = new GenericRepository<Team>(context);

            football = new Sport() { Id = 1, Name = "Football", EncounterMode = EncounterMode.Double };
            mockSportBL.Setup(s => s.GetById(1)).Returns(football);

            nacional = new Team { Id = 1, Name = "Nacional", SportId = 1 };
            peñarol = new Team { Id = 2, Name = "Peñarol", SportId = 1 };
            defensor = new Team { Id = 3, Name = "Defensor", SportId = 1 };
            danubio = new Team { Id = 4, Name = "Danubio", SportId = 1 };
            cerro = new Team { Id = 5, Name = "Cerro", SportId = 1 };
            freeForAll = new FreeForAll(encounterBL);

            teamList = new List<Team>() { nacional, peñarol, danubio, defensor, cerro };
        }

        // [TestCleanup]
        // public void TestCleanup()
        // {
        //     context.RemoveRange(encounterRepository.Get(null, null, ""));
        //     context.SaveChanges();
        // }

        [TestMethod]
        public void GenerateFixtureWithNoEncountersOnRepoTest()
        {
            DateTime date = new DateTime(2018, 10, 1, 12, 00, 00);
            ICollection<Encounter> encounters = freeForAll.GenerateFixture(teamList, date);
            int count = encounters.Count;
            var expectedGeneratedEncountersCount = (teamList.Count() * (teamList.Count() - 1)) / 2;
            Assert.IsTrue(count == expectedGeneratedEncountersCount);
        }

        [TestMethod]
        public void GenerateFixtureWithEncountersOnRepoTest()
        {
            DateTime date = new DateTime(2018, 10, 1, 12, 00, 00);
            EncountersTeams etNacional = new EncountersTeams { Team = nacional, TeamId = nacional.Id };
            EncountersTeams etPeñarol = new EncountersTeams { Team = peñarol, TeamId = peñarol.Id };
            encounterBL.Add(new Encounter { Id = 1, Teams = { etNacional, etPeñarol }, Date = date, SportId = 1 });
            ICollection<Encounter> generatedEncounters = freeForAll.GenerateFixture(teamList, date);
            List<Encounter> encountersToList = generatedEncounters.ToList();
            //Sabemos que el primer partido va a ser Nacional Peñarol porque ya esta en el repositorio
            //entonces la fecha del primer encuentro generado por el algoritmo debe ser un dia mas.
            Assert.IsTrue(generatedEncounters.ElementAt(0).Date == new DateTime(2018, 10, 2, 12, 00, 00));
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughTeamsForEncounterException))]
        public void GenerateRoundRobinWithOneTeamTest()
        {
            teamList.Clear();
            teamList.Add(nacional);
            var encounters = freeForAll.GenerateFixture(teamList, DateTime.Now);
            var expectedGeneratedEncountersCount = (teamList.Count() * (teamList.Count() - 1)) / 2;
            Assert.IsTrue(encounters.Count == expectedGeneratedEncountersCount);
        }
    }
}