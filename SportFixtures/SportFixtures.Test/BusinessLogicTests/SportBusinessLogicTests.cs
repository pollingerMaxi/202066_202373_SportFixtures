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

        [TestMethod]
        public void UniqueNameTest(){
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.SportRepository);
            
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockUnitOfWork.Object);

            string sportName = "Futbol";

            Assert.AreEqual(sportBL.UniqueName(sportName), true);
        }
        
        [TestMethod]
        public void AddSportTest()
        {
            var sportName = "Futbol";
            var mockUnitOfWork = new Mock<UnitOfWork>(new Context());
            mockUnitOfWork.Setup(un => un.SportRepository.Insert(new Sport() { Name = sportName}));
            mockUnitOfWork.Setup(un => un.Save());
            ISportBusinessLogic sportBL = new SportBusinessLogic(mockUnitOfWork.Object);
            sportBL.AddSport(sportName);
            Assert.IsTrue(mockUnitOfWork.Object.SportRepository.Get(null, null, "").First().Name == sportName);
        }

    }
}
