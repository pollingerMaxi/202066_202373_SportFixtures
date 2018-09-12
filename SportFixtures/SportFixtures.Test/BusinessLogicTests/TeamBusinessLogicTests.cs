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
        private UnitOfWork unitOfWork; 

        [TestInitialize]
        public void TestInitialize()
        {
            context = new Context();
            unitOfWork = new UnitOfWork(context);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var teams = unitOfWork.TeamRepository.Get();
            context.RemoveRange(teams);
            context.SaveChanges();
        }

        

    }
}
