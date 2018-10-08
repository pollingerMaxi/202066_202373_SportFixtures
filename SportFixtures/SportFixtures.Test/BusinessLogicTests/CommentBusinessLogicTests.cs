using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.CommentExceptions;
using SportFixtures.Exceptions.UserExceptions;
using SportFixtures.Exceptions.EncounterExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace SportFixtures.Test.BusinessLogicTests
{
    [TestClass]
    public class CommentBusinessLogicTests
    {
        private const dynamic NO_BUSINESS_LOGIC = null;
        private const dynamic NO_UT_REPOSITORY = null;
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
        private User user;
        private Encounter encounter;

        [TestInitialize]
        public void TestInitialize()
        {
            mockCommentRepo = new Mock<IRepository<Comment>>();
            mockEncounterRepo = new Mock<IRepository<Encounter>>();
            mockSportRepo = new Mock<IRepository<Sport>>();
            mockTeamRepo = new Mock<IRepository<Team>>();
            mockUserRepo = new Mock<IRepository<User>>();
            encounterBL = new EncounterBusinessLogic(mockEncounterRepo.Object, NO_BUSINESS_LOGIC);
            sportBL = new SportBusinessLogic(mockSportRepo.Object);
            teamBL = new TeamBusinessLogic(mockTeamRepo.Object, sportBL);
            userBL = new UserBusinessLogic(mockUserRepo.Object, teamBL, NO_UT_REPOSITORY);
            commentBL = new CommentBusinessLogic(mockCommentRepo.Object, encounterBL, userBL);
            commentList = new List<Comment>();
            user = new User() { Id = 1 };
            var team1 = new Team() { Id = 1, Name = "Nacional", SportId = 1 };
            var team2 = new Team() { Id = 2, Name = "Peñarol", SportId = 1 };
            var sport = new Sport() { Id = 1, Name = "Futbol" };
            encounter = new Encounter() { Id = 1, Date = DateTime.Now, SportId = sport.Id, Team1 = team1, Team2 = team2 };
            mockCommentRepo.Setup(r => r.Get(null, null, "")).Returns(commentList);
        }

        [TestMethod]
        public void AddCommentOkTest()
        {
            Comment comment = new Comment() { Id = 1, EncounterId = 1, UserId = 1, Text = "This is a comment." };
            mockCommentRepo.Setup(x => x.Insert(It.IsAny<Comment>())).Callback<Comment>(x => commentList.Add(comment));
            mockEncounterRepo.Setup(e => e.Update(It.IsAny<Encounter>())).Callback<Encounter>(e => encounter.Comments.Add(comment));
            mockEncounterRepo.Setup(e => e.GetById(It.IsAny<int>())).Returns(encounter);
            mockUserRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);
            commentBL.Add(comment);
            mockCommentRepo.Verify(x => x.Insert(It.IsAny<Comment>()), Times.Once());
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

        [TestMethod]
        [ExpectedException(typeof(UserDoesNotExistException))]
        public void AddCommentUserDoesntExistsShouldReturnExceptionTest()
        {
            Comment comment = new Comment() { Id = 1, EncounterId = 1, UserId = 1, Text = "This is a comment." };
            mockCommentRepo.Setup(x => x.Insert(It.IsAny<Comment>())).Callback<Comment>(x => commentList.Add(comment));
            mockEncounterRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(encounter);
            commentBL.Add(comment);
        }

        [TestMethod]
        [ExpectedException(typeof(EncounterDoesNotExistException))]
        public void AddCommentEncounterDoesntExistsShouldReturnExceptionTest()
        {
            Comment comment = new Comment() { Id = 1, EncounterId = 1, UserId = 1, Text = "This is a comment." };
            mockCommentRepo.Setup(x => x.Insert(It.IsAny<Comment>())).Callback<Comment>(x => commentList.Add(comment));
            mockUserRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(user);
            commentBL.Add(comment);
        }

        [TestMethod]
        public void GetAllCommentsTest()
        {
            var comments = commentBL.GetAll();
            mockCommentRepo.Verify(x => x.Get(null, null, ""), Times.Once());
        }

        [TestMethod]
        public void GetCommentByIdTest()
        {
            var comment = commentBL.GetById(1);
            mockCommentRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
        }
        
        [TestMethod]
        public void GetAllCommentsOfEncounterTest()
        {
            var comments = commentBL.GetAllCommentsOfEncounter(1);
            mockCommentRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Comment, bool>>>(), null, ""), Times.Once());
        }
    }
}