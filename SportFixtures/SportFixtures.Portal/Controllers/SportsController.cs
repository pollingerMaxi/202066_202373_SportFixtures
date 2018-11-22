using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Enums;
using SportFixtures.Exceptions.SportExceptions;
using SportFixtures.Portal.DTOs;
using SportFixtures.Portal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Controllers
{
    [Route("api/sports")]
    public class SportsController : ControllerBase
    {
        private ISportBusinessLogic sportBusinessLogic;
        private readonly IMapper mapper;

        public SportsController(ISportBusinessLogic sportBL, IMapper mapper)
        {
            sportBusinessLogic = sportBL;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ICollection<SportDTO>> GetAllSports()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var sports = mapper.Map<SportDTO[]>(sportBusinessLogic.GetAll());
                return Ok(sports);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<SportDTO> GetSport(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var sport = mapper.Map<SportDTO>(sportBusinessLogic.GetById(id));
                return Ok(sport);
            }
            catch (SportDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [AuthorizedRoles(Role.Admin)]
        public ActionResult CreateSport([FromBody]SportCreateDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var sport = mapper.Map<Sport>(data);
                sportBusinessLogic.Add(sport);
                return Ok(mapper.Map<SportDTO>(sport));
            }
            catch (SportException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [AuthorizedRoles(Role.Admin)]
        public ActionResult UpdateSport([FromBody]SportDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var sport = mapper.Map<Sport>(data);
                sportBusinessLogic.Update(sport);
                return Ok(mapper.Map<SportDTO>(sport));
            }
            catch (SportDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (SportException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        [AuthorizedRoles(Role.Admin)]
        public ActionResult DeleteSport(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                sportBusinessLogic.Delete(id);
                return Ok(new ResponseOkDTO());
            }
            catch (SportDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
