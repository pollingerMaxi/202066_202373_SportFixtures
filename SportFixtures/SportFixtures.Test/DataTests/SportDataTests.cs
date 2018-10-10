using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SportFixtures.Test.DataTests
{
    [TestClass]
    public class SportDataTests
    {
        private Context context;
        private IRepository<Sport> repository;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "sportDB").Options;
            context = new Context(options);
            repository = new GenericRepository<Sport>(context);
        }

        [TestMethod]
        public void GetSportsWithNoSportsInRepositoryTest()
        {
            var sport = new Sport();
            var list = new List<Sport>();
            var mockRepo = new Mock<IRepository<Sport>>();
            mockRepo.Setup(r => r.Get(null, null, "")).Returns(list);
            var sports = mockRepo.Object.Get();
            mockRepo.Verify(x => x.Get(null, null, ""));
            Assert.IsTrue(sports.Count() == 0);
        }

        [TestMethod]
        public void GetSportsWithOneSportInRepositoryTest()
        {
            var sport = new Sport();
            var list = new List<Sport>() { sport };
            var mockRepo = new Mock<IRepository<Sport>>();
            mockRepo.Setup(r => r.Get(null, null, "")).Returns(list);
            var sports = mockRepo.Object.Get();
            mockRepo.Verify(x => x.Get(null, null, ""));
            Assert.IsTrue(sports.Count() == 1);
        }

        [TestMethod]
        public void AddSportWithNameTest()
        {
            var sport = new Sport();
            var list = new List<Sport>();
            var mockRepo = new Mock<IRepository<Sport>>();
            mockRepo.Setup(r => r.Insert(It.IsAny<Sport>())).Callback<Sport>(x => list.Add(sport));
            mockRepo.Setup(r => r.Get(null, null, "")).Returns(list);
            mockRepo.Object.Insert(It.IsAny<Sport>());
            var sports = mockRepo.Object.Get();
            mockRepo.Verify(x => x.Insert(It.IsAny<Sport>()));
            mockRepo.Verify(x => x.Get(null, null, ""));
            Assert.IsTrue(sports.Count() == 1);
            //var sport = new Sport() { Name = "SportName" };
            repository.Insert(sport);
        }

        //[TestMethod]
        //public void GetSportsIncludeTeamsTest()
        //{
        //    var mockRepo = new Mock<IRepository<Sport>>();
        //    var list = new List<Sport>() { new Sport() { Name = "name" } };
        //    //mockRepo.Setup(r => r.Get(null, null, It.IsAny<string>())).Returns(list);
        //    var sports = (List<Sport>)mockRepo.Object.Get(null, null, "Teams");
        //    //mockRepo.Verify(x => x.Get(null, null, It.IsAny<string>()), Times.Once);
        //    Assert.IsTrue(sports.Count == 1);
        //}

        [TestMethod]
        public void AttachSportTest()
        {
            var sport = new Sport();
            repository.Attach(sport);
            repository.Save();
            Assert.IsTrue(context.Entry(sport).State == EntityState.Unchanged);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void DisposeRepositoryTest()
        {
            repository.Dispose();
            repository.Insert(new Sport());
        }
    }
}
