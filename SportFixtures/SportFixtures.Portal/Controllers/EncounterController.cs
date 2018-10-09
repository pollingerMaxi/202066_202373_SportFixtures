using Microsoft.AspNetCore.Mvc;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data;
using SportFixtures.Data.Entities;
using SportFixtures.Exceptions.EncounterExceptions;
using SportFixtures.Exceptions.SportExceptions;
using SportFixtures.Portal.DTOs;
using SportFixtures.Portal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Controllers
{
    [Route("api/encounters")]
    public class EncounterController : ControllerBase
    {
        private IEncounterBusinessLogic encounterBusinessLogic;

        public EncounterController(IEncounterBusinessLogic encounterBL)
        {
            this.encounterBusinessLogic = encounterBL;
        }

        [HttpGet]
        public ActionResult<ICollection<Encounter>> GetAllEncounters()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var encounters = encounterBusinessLogic.GetAll();
                return Ok(encounters);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Encounter> GetEncounterById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var encounter = encounterBusinessLogic.GetById(id);
                return Ok(encounter);
            }
            catch (EncounterDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [AuthorizedRoles(Role.Admin)]
        public ActionResult CreateEncounter([FromBody]Encounter encounter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                encounterBusinessLogic.Add(encounter);
                return Ok();
            }
            catch (EncounterException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [AuthorizedRoles(Role.Admin)]
        public ActionResult UpdateEncounter(int id, [FromBody]Encounter encounter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                encounterBusinessLogic.Update(encounter);
                return Ok();
            }
            catch (EncounterDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (EncounterException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [AuthorizedRoles(Role.Admin)]
        public ActionResult DeleteEncounter(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                encounterBusinessLogic.Delete(id);
                return Ok();
            }
            catch (EncounterDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("sports/{sportId}")]
        public ActionResult GetAllEncountersOfSport(int sportId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var encounters = encounterBusinessLogic.GetAllEncountersOfSport(sportId);
                return Ok();
            }
            catch (NoEncountersFoundForSportException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("teams/{teamId}")]
        public ActionResult GetAllEncountersOfTeam(int teamId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var encounters = encounterBusinessLogic.GetAllEncountersOfTeam(teamId);
                return Ok();
            }
            catch (NoEncountersFoundForTeamException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("bydate/{date}")]
        public ActionResult GetAllEncountersOfTheDay(DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var encounters = encounterBusinessLogic.GetAllEncountersOfTheDay(date);
                return Ok();
            }
            catch (NoEncountersFoundForDateException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("fixture/")]
        public ActionResult GenerateFixture([FromBody] FixtureDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var encounters = encounterBusinessLogic.GenerateFixture(data.Date, data.SportId, data.Algorithm);
                return Ok(encounters);
            }
            catch (SportDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (FixtureGeneratorAlgorithmDoesNotExist e)
            {
                return NotFound(e.Message);
            }
            catch (NotEnoughTeamsForEncounterException e)
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