using Microsoft.AspNetCore.Mvc;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Exceptions.EncounterExceptions;
using SportFixtures.Exceptions.SportExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Controllers
{
    [Route("api/positions")]
    public class PositionsController : ControllerBase
    {
        private IPositionTableCalculator tableCalculator;

        public PositionsController(IPositionTableCalculator tCalculator)
        {
            this.tableCalculator = tCalculator;
        }

        [HttpGet("{id}")]
        public ActionResult GetPositionsTableForSport(int sportId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var table = tableCalculator.GeneratePositionTableForSport(sportId);
                return Ok(table);
            }
            catch (SportDoesNotExistException e)
            {
                return NotFound(e.Message);
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
    }
}
