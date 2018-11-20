using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.Data;
using SportFixtures.Data.Entities;
using SportFixtures.Exceptions.EncounterExceptions;
using SportFixtures.Exceptions.SportExceptions;
using SportFixtures.Portal.DTOs;
using SportFixtures.Portal.Filters;
using SportFixtures.FixtureGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Controllers
{
    [Route("api/fixtures")]
    public class FixtureGeneratorController : ControllerBase
    {
        private IFixtureSelector fixtureSelector;
        private ISportBusinessLogic sportBL;
        private readonly IMapper mapper;

        public FixtureGeneratorController(IFixtureSelector fixtureSelector, ISportBusinessLogic sportBL, IMapper mapper)
        {
            this.fixtureSelector = fixtureSelector;
            this.sportBL = sportBL;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ICollection<FixtureDTO>> GetAllFixtureGenerators()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var encounters = mapper.Map<Fixture[]>(encounterBusinessLogic.GetAll());
                return Ok(encounters);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("generate/")]
        [AuthorizedRoles(Role.Admin)]
        public ActionResult<ICollection<EncounterDTO>> GenerateFixture([FromBody]FixtureDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                fixtureSelector.CreateInstance(data.AlgorithmName);
                ICollection<Team> teams = sportBL.GetById(data.SportId).Teams;
                var encounters = mapper.Map<EncounterDTO[]>(fixtureSelector.GenerateFixture(teams, data.Date));
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
                return StatusCode(500, e.Message);
            }
        }
    }
}