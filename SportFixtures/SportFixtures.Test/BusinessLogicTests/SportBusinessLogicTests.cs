using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.BusinessLogic.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportFixtures.Test.BusinessLogicTests
{
    [TestClass]
    public class SportBusinessLogicTests
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
            var sports = unitOfWork.SportRepository.Get();
            context.RemoveRange(sports);
            context.SaveChanges();
        }

        [TestMethod]
        public void UniqueNameTest(){
            var mockUnitOfWork = new Mock<UnitOfWork>();
            mockUnitOfWork.Setup(un => un.SportRepository.Get(null, null, ""));
            
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockUnitOfWork.Object);
            string sportName = "Futbol";

            Assert.Equals(sportBL.UniqueName(sportName), true);
        }
        

    }
}
