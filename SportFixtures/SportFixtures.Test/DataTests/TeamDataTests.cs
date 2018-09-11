using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportFixtures.Test.DataTests
{
    [TestClass]
    public class TeamDataTests
    {
        [TestCleanup]
        public void TestCleanup()
        {
            using (var context = new Context())
            {
                var unitOfWork = new UnitOfWork(context);
                var teams = unitOfWork.TeamRepository.Get();
                context.RemoveRange(teams);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetAllTeamsWithNoTeamsInRepository()
        {
            var context = new Context();
            var unitOfWork = new UnitOfWork(context);
            var teams = unitOfWork.TeamRepository.Get();
            Assert.IsTrue(teams.Count() == 0);
        }

        [TestMethod]
        public void GetAllTeamsWithOneTeamInRepository()
        {
            var context = new Context();
            var unitOfWork = new UnitOfWork(context);
            unitOfWork.TeamRepository.Insert(new Team());
            context.SaveChanges();
            var teams = unitOfWork.TeamRepository.Get();
            Assert.IsTrue(teams.Count() == 1);
        }
    }
}
