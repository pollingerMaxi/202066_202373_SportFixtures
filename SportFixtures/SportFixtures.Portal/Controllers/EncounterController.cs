using Microsoft.AspNetCore.Mvc;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Exceptions.EncounterExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Controllers
{
    [Route("api/encounter")]
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
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
        public ActionResult UpdateSport(int id, [FromBody]Encounter encounter)
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
            catch (EncounterException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
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
            catch (EncounterException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("sports/{sportId}")]
        public ActionResult GetAllEncountersOfSport(int sportId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var encounters = encounterBusinessLogic.GetAllEncountersOfSport(sportId);
            return Ok();
            //ACA CAPAZ TENDRIAMOS QUE CHEQUEAR QUE EXISTA EL SPORT
        }

        [HttpDelete("teams/{sportId}")]
        public ActionResult GetAllEncountersOfTeam(int teamId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var encounters = encounterBusinessLogic.GetAllEncountersOfTeam(teamId);
            return Ok();
            //ACA CAPAZ TENDRIAMOS QUE CHEQUEAR QUE EXISTA EL TEAM
        }

        [HttpDelete("teams/{sportId}")]
        public ActionResult GetAllEncountersOfTheDay(DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var encounters = encounterBusinessLogic.GetAllEncountersOfTheDay(date);
            return Ok();
        }
    }
}