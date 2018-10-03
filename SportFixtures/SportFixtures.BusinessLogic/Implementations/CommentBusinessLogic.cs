using System;
using SportFixtures.Data.Entities;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Repository;
using SportFixtures.Exceptions.CommentExceptions;
using System.Linq;
using System.Collections.Generic;

namespace SportFixtures.BusinessLogic.Implementations
{
    public class CommentBusinessLogic : ICommentBusinessLogic
    {
        private IRepository<Comment> repository;
        private IEncounterBusinessLogic encounterBL;
        private IUserBusinessLogic userBL;

        public CommentBusinessLogic(IRepository<Comment> repository, IEncounterBusinessLogic encounterBL, IUserBusinessLogic userBL)
        {
            this.repository = repository;
            this.encounterBL = encounterBL;
            this.userBL = userBL;
        }

        public void Add(Comment comment){
            Validate(comment);
            encounterBL.AddCommentToEncounter(comment);
            repository.Insert(comment);
            repository.Save();
        }
        
        private void Validate(Comment comment){
            if(string.IsNullOrWhiteSpace(comment.Text)){
                throw new InvalidCommentTextException();
            }
            encounterBL.CheckIfExists(comment.EncounterId);
            userBL.CheckIfExists(comment.UserId);
        }

        public IEnumerable<Comment> GetAll(){
            return repository.Get(null, null, "");
        }
        public Comment GetById(int id){
            return repository.GetById(id);
        }
        public IEnumerable<Comment> GetAllCommentsOfEncounter(int encounterId)
        {
            return repository.Get(c => (c.EncounterId == encounterId), null, "");
        }

    }
}