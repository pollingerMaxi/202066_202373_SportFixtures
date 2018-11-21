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
using SportFixtures.Exceptions.FixtureSelectorExceptions;
using SportFixtures.Data.Enums;

namespace SportFixtures.Portal.Controllers
{
    [Route("api/fixtures")]
    public class FixtureGeneratorController : ControllerBase
    {
        private IFixtureSelector fixtureSelector;
        private IEncounterBusinessLogic encounterBL;
        private ISportBusinessLogic sportBL;
        private readonly IMapper mapper;

        public FixtureGeneratorController(IFixtureSelector fixtureSelector, IEncounterBusinessLogic encounterBL, ISportBusinessLogic sportBL, IMapper mapper)
        {
            this.fixtureSelector = fixtureSelector;
            this.encounterBL = encounterBL;
            this.sportBL = sportBL;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ICollection<string>> GetAllFixtureGenerators()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var fixtures = fixtureSelector.GetAlgorithmNames();
                return Ok(fixtures);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("generate")]
        [AuthorizedRoles(Role.Admin)]
        public ActionResult<ICollection<FixtureEncounterDTO>> GenerateFixture([FromBody]FixtureDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                fixtureSelector.CreateInstance(data.AlgorithmName, encounterBL);
                var teams = sportBL.GetById(data.SportId).Teams;
                var encounters = mapper.Map<FixtureEncounterDTO[]>(fixtureSelector.GenerateFixture(teams, data.Date));
                var response = Ok(encounters);
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
            catch (AlgorithmDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (ThereAreNoAlgorithmsException e)
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