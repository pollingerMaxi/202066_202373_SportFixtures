using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.Data;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Enums;
using SportFixtures.Exceptions.EncounterExceptions;
using SportFixtures.Exceptions.SportExceptions;
using SportFixtures.Logger;
using SportFixtures.Portal.DTOs;
using SportFixtures.Portal.Filters;
using SportFixtures.FixtureGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Controllers
{
    [Route("api/encounters")]
    public class EncounterController : ControllerBase
    {
        private static readonly string ACTION = "Fixture";

        private IEncounterBusinessLogic encounterBusinessLogic;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public EncounterController(IEncounterBusinessLogic encounterBL, IMapper mapper, ILogger logger)
        {
            this.encounterBusinessLogic = encounterBL;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<ICollection<EncounterDTO>> GetAllEncounters()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var encounters = mapper.Map<EncounterDTO[]>(encounterBusinessLogic.GetAll());
                return Ok(encounters);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<EncounterDTO> GetEncounterById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var encounter = mapper.Map<EncounterDTO>(encounterBusinessLogic.GetById(id));
                return Ok(encounter);
            }
            catch (EncounterDoesNotExistException e)
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
        public ActionResult CreateEncounter([FromBody]EncounterDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var encounter = mapper.Map<Encounter>(data);
                encounterBusinessLogic.Add(encounter);
                return Ok(mapper.Map<EncounterDTO>(encounter));
            }
            catch (EncounterException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("add_many")]
        [AuthorizedRoles(Role.Admin)]
        public ActionResult CreateManyEncounters([FromBody]ICollection<EncounterDTO> data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var encounter = mapper.Map<ICollection<Encounter>>(data);
                encounterBusinessLogic.AddMany(encounter);
                return Ok(mapper.Map<ICollection<EncounterDTO>>(encounter));
            }
            catch (EncounterException e)
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
        public ActionResult UpdateEncounter([FromBody]EncounterDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var encounter = mapper.Map<Encounter>(data);
                encounterBusinessLogic.Update(encounter);
                return Ok(mapper.Map<EncounterDTO>(encounter));
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
                return StatusCode(500, e.Message);
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
                return Ok(new ResponseOkDTO());
            }
            catch (EncounterDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("sport/{sportId}")]
        [AuthorizedRoles(Role.Admin)]
        public ActionResult<ICollection<EncounterDTO>> GetAllEncountersOfSport(int sportId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var encounters = mapper.Map<EncounterDTO[]>(encounterBusinessLogic.GetAllEncountersOfSport(sportId));
                return Ok(encounters);
            }
            catch (NoEncountersFoundForSportException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("team/{teamId}")]
        public ActionResult<ICollection<EncounterDTO>> GetAllEncountersOfTeam(int teamId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var encounters = mapper.Map<EncounterDTO[]>(encounterBusinessLogic.GetAllEncountersOfTeam(teamId));
                return Ok(encounters);
            }
            catch (NoEncountersFoundForTeamException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("bydate/{date}")]
        public ActionResult<ICollection<EncounterDTO>> GetAllEncountersOfTheDay(DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var encounters = mapper.Map<EncounterDTO[]>(encounterBusinessLogic.GetAllEncountersOfTheDay(date));
                return Ok(encounters);
            }
            catch (NoEncountersFoundForDateException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("results")]
        [AuthorizedRoles(Role.Admin)]
        public ActionResult AddResultsToEncounter([FromBody]ResultsDTO results)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                encounterBusinessLogic.AddResults(results.Positions, results.EncounterId);
                return Ok(new ResponseOkDTO());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
