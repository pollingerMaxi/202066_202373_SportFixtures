using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Exceptions.CommentExceptions;
using SportFixtures.Portal.DTOs;
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
        private readonly IMapper mapper;

        public CommentsController(ICommentBusinessLogic commentsBL, IMapper mapper)
        {
            commentBusinessLogic = commentsBL;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ICollection<CommentDTO>> GetAllComments()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var comments = mapper.Map<CommentDTO[]>(commentBusinessLogic.GetAll());
                return Ok(comments);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CommentDTO> GetComment(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var comment = mapper.Map<CommentDTO>(commentBusinessLogic.GetById(id));
                return Ok(comment);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateComment([FromBody]CommentDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var comment = mapper.Map<Comment>(data);
                commentBusinessLogic.Add(comment);
                return Ok(mapper.Map<CommentDTO>(comment));
            }
            catch (CommentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("encounter/{encounterId}")]
        public ActionResult<ICollection<CommentDTO>> GetCommentsOfEncounter(int encounterId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var comments = mapper.Map<CommentDTO[]>(commentBusinessLogic.GetAllCommentsOfEncounter(encounterId));
                return Ok(comments);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
