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

namespace SportFixtures.Test.FreeForAllTests
{
    [TestClass]
    public class FreeForAllTests
    {
        private Context context;
        private IEncounterBusinessLogic encounterBL;
        private IRepository<Encounter> encounterRepository;
        private IRepository<Team> teamRepository;
        private IFixtureGenerator freeForAll;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "encounterDB").Options;
            context = new Context(options);
            encounterRepository = new GenericRepository<Encounter>(context);
            encounterBL = new EncounterBusinessLogic(encounterRepository);
            teamRepository = new GenericRepository<Team>(context);
            //encounterList = new List<Encounter>();
            
            //mockEncounterRepo.Setup(r => r.Get(null, null, "")).Returns(encounterList);
            //mockEncounterRepo.Setup(r => r.Get(null, null, "")).Returns(encounterList);

            //Team nacional = new Team{Id = 1, Name = "Nacional", SportId = 1};
            //Team peñarol = new Team{Id = 2, Name = "Peñarol", SportId = 1};
            //Team defensor = new Team{Id = 3, Name = "Defensor", SportId = 1};
            //Team danubio = new Team{Id = 4, Name = "Danubio", SportId = 1};
            freeForAll = new FreeForAll(encounterBL);

            
            //teamList = new List<Team>(){nacional, peñarol, danubio, defensor};
           
        }

        [TestMethod]
        public void GenerateFixtureOk(){
            teamRepository.Insert(new Team{Id = 1, Name = "Nacional", SportId = 1});
            teamRepository.Insert(new Team{Id = 2, Name = "Peñarol", SportId = 1});
            teamRepository.Insert(new Team{Id = 3, Name = "Defensor", SportId = 1});
            teamRepository.Insert(new Team{Id = 4, Name = "Danubio", SportId = 1});
            IEnumerable<Team> teamList = teamRepository.Get(null, null, "");
            ICollection<Encounter> encounters = freeForAll.GenerateFixture(teamList, DateTime.Now, 1);
            int count = encounters.Count;
            Assert.IsTrue(count == 6);
        }
    }
}