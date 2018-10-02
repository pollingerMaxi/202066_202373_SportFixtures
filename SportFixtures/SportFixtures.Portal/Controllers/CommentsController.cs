using Microsoft.AspNetCore.Mvc;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Exceptions.CommentExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Controllers
{
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        private ICommentBusinessLogic commentBusinessLogic;

        public CommentsController(ICommentBusinessLogic commentsBL)
        {
            commentBusinessLogic = commentsBL;
        }

        [HttpGet]
        public ActionResult<ICollection<Comment>> GetAllComments()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //var comments = commentBusinessLogic.GetAll();
                return null; // Ok(comments);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Comment> GetComment(int id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //try
            //{
            //    var comment  = commentBusinessLogic.GetById(id);
            //    return Ok(comment);
            //}
            //catch (Exception e)
            //{
            //    return BadRequest(e.Message);
            //}
            return null;
        }

        [HttpPost]
        public ActionResult CreateComment([FromBody]Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                commentBusinessLogic.Add(comment);
                return Ok();
            }
            catch (CommentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
