using Microsoft.AspNetCore.Mvc;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Exceptions.SportExceptions;
using SportFixtures.Exceptions.TeamExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Controllers
{
    [Route("api/teams")]
    public class TeamsController : ControllerBase
    {
        private ITeamBusinessLogic teamBusinessLogic;

        public TeamsController(ITeamBusinessLogic teamBL)
        {
            teamBusinessLogic = teamBL;
        }

        [HttpGet]
        public ActionResult<ICollection<Team>> GetAllTeams()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var teams = teamBusinessLogic.GetAll();
                return Ok(teams);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Team> GetTeam(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var team = teamBusinessLogic.GetById(id);
                return Ok(team);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateTeam([FromBody]Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                teamBusinessLogic.Add(team);
                return Ok();
            }
            catch (TeamException e)
            {
                return BadRequest(e.Message);
            }
            catch (SportDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (TeamAlreadyInSportException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTeam(int id, [FromBody]Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                teamBusinessLogic.Update(team);
                return Ok();
            }
            catch (TeamDoesNotExistsException e)
            {
                return NotFound(e.Message);
            }
            catch (TeamException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTeam(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                teamBusinessLogic.Delete(id);
                return Ok();
            }
            catch (TeamDoesNotExistsException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
