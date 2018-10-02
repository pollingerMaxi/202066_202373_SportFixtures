using Microsoft.AspNetCore.Mvc;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Exceptions.UserExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Controllers
{
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private IUserBusinessLogic userBusinessLogic;

        public LoginController(IUserBusinessLogic userBL)
        {
            userBusinessLogic = userBL;
        }

        [HttpPost]
        public ActionResult Login([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                userBusinessLogic.Login(user);
                return Ok();
            }
            catch (UserDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (EmailOrPasswordException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
