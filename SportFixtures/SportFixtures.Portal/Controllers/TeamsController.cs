using Microsoft.AspNetCore.Mvc;
using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Controllers
{
    [Route("api/teams")]
    public class TeamsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<ICollection<Team>> GetAllTeams()
        {
            return new List<Team>() { new Team(), new Team() };
        }

        [HttpGet("{id}")]
        public ActionResult<Team> GetTeam(int id)
        {
            return new Team() { Id = id };
        }

        [HttpPost]
        public void CreateTeam([FromBody]Team team)
        {
            //TeamBL.Add(team);
        }

        [HttpPut("{id}")]
        public void UpdateTeam(int id, [FromBody]Team team)
        {
            //TeamBL.Update(team);
        }

        [HttpDelete("{id}")]
        public void DeleteTeam(int id)
        {
            //TeamBL.Delete(new Team() { Id = id });
        }
    }
}
