using Microsoft.AspNetCore.Mvc;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Exceptions.SportExceptions;
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

        public SportsController(ISportBusinessLogic sportBL)
        {
            sportBusinessLogic = sportBL;
        }

        [HttpGet]
        public ActionResult<ICollection<Sport>> GetAllSports()
        {
            return null;
        }

        [HttpGet("{id}")]
        public ActionResult<Sport> GetSport(int id)
        {
            return null;
        }

        [HttpPost]
        public ActionResult CreateSport([FromBody]Sport sport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                sportBusinessLogic.Add(sport);
                return Ok();
            }
            catch (SportException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public void UpdateSport(int id, [FromBody]Sport sport)
        {
            //SportBL.Update(sport);
        }

        [HttpDelete("{id}")]
        public void DeleteSport(int id)
        {
            //SportBL.Delete(new Sport() { Id = id });
        }
    }
}
