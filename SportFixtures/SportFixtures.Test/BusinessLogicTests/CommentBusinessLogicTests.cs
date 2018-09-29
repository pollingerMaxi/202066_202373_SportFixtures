using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.CommentExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportFixtures.Test.BusinessLogicTests
{
    [TestClass]
    public class CommentBusinessLogicTests
    {
        private const dynamic NO_BUSINESS_LOGIC = null;
        private Context context;
        private IRepository<Comment> repository;
        private Mock<IRepository<Comment>> mockCommentRepo;
        private Mock<IRepository<Encounter>> mockEncounterRepo;
        private Mock<IRepository<Sport>> mockSportRepo;
        private Mock<IRepository<Team>> mockTeamRepo;
        private Mock<IRepository<User>> mockUserRepo;
        private ICommentBusinessLogic commentBL;
        private IEncounterBusinessLogic encounterBL;
        private ISportBusinessLogic sportBL;
        private ITeamBusinessLogic teamBL;
        private IUserBusinessLogic userBL;
        private List<Comment> commentList;

        [TestInitialize]
        public void TestInitialize(){
            mockCommentRepo = new Mock<IRepository<Comment>>();
            mockEncounterRepo = new Mock<IRepository<Encounter>>();
            mockSportRepo = new Mock<IRepository<Sport>>();
            mockTeamRepo = new Mock<IRepository<Team>>();
            mockUserRepo = new Mock<IRepository<User>>();
            encounterBL = new EncounterBusinessLogic(mockEncounterRepo.Object);
            sportBL = new SportBusinessLogic(mockSportRepo.Object);
            teamBL = new TeamBusinessLogic(mockTeamRepo.Object, sportBL);
            userBL = new UserBusinessLogic(mockUserRepo.Object, teamBL);
            commentBL = new CommentBusinessLogic(mockCommentRepo.Object, encounterBL, userBL);
            commentList = new List<Comment>();
            mockCommentRepo.Setup(r => r.Get(null, null, "")).Returns(commentList);
        }

        [TestMethod]
        public void AddCommentOkTest()
        {
            Comment comment = new Comment() { Id = 1, EncounterId = 1, UserId = 1, Text = "This is a comment." };
            mockCommentRepo.Setup(x => x.Insert(It.IsAny<Comment>())).Callback<Comment>(x => commentList.Add(comment));
            commentBL.Add(comment);
            mockCommentRepo.Verify(x => x.Insert(It.IsAny<Comment>()), Times.Once());
            mockCommentRepo.Verify(x => x.Save(), Times.Once());
            Assert.IsTrue(commentList.First().Id == comment.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommentTextException))]
        public void AddCommentEmptyTextShouldReturnExceptionTest()
        {
            Comment comment = new Comment() { Id = 1, EncounterId = 1, UserId = 1, Text = "" };
            mockCommentRepo.Setup(x => x.Insert(It.IsAny<Comment>())).Callback<Comment>(x => commentList.Add(comment));
            commentBL.Add(comment);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommentTextException))]
        public void AddCommentTextOnlyWhiteSpacesShouldReturnExceptionTest()
        {
            Comment comment = new Comment() { Id = 1, EncounterId = 1, UserId = 1, Text = "      " };
            mockCommentRepo.Setup(x => x.Insert(It.IsAny<Comment>())).Callback<Comment>(x => commentList.Add(comment));
            commentBL.Add(comment);
        }
    }
}