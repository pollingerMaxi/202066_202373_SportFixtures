using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportFixtures.Data.Access;
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
        [TestMethod]
        public void GetAllTeamsWithNoTeamsInRepository()
        {
            var context = new Context();
            var unitOfWork = new UnitOfWork(context);
            var teams = unitOfWork.TeamRepository.Get();
            Assert.IsTrue(teams.Count() == 0);
        }
    }
}
