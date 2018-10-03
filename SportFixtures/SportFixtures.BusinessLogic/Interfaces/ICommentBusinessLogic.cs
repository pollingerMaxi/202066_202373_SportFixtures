using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface ICommentBusinessLogic
    {
        void Add(Comment comment);
        IEnumerable<Comment> GetAll();
        Comment GetById(int id);
        IEnumerable<Comment> GetAllCommentsOfEncounter(int encounterId);
    }
}