using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface ICommentBusinessLogic
    {
        /// <summary>
        /// Adds a comment.
        /// </summary>
        /// <param name="comment"></param>
        void Add(Comment comment);

        /// <summary>
        /// Returns all the comments in the repository.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Comment> GetAll();
        
        /// <summary>
        /// Returns the comment with given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Comment GetById(int id);

        /// <summary>
        /// Returns all the comments of the given encounter.
        /// </summary>
        /// <param name="encounterId"></param>
        /// <returns></returns>
        IEnumerable<Comment> GetAllCommentsOfEncounter(int encounterId);
    }
}