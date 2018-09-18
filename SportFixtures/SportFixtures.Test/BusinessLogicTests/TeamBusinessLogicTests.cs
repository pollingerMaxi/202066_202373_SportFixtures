using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class TeamBusinessLogicTests
    {
        private Context context;
        private IRepository<Team> repository; 

        [TestInitialize]
        public void TestInitialize()
        {
            context = new Context();
            repository = new GenericRepository<Team>(context);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var teams = repository.Get();
            context.RemoveRange(teams);
            context.SaveChanges();
        }

        

    }
}
