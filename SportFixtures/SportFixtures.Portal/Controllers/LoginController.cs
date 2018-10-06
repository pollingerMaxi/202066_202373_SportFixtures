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
    public class LoginController : ControllerBase
    {
        private IUserBusinessLogic userBusinessLogic;

        public LoginController(IUserBusinessLogic userBL)
        {
            userBusinessLogic = userBL;
        }

        [HttpPost]
        [Route("api/login")]
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

        [HttpPost]
        [Route("api/logout")]
        public ActionResult Logout([FromBody]string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                userBusinessLogic.Logout(email);
                return Ok();
            }
            catch (UserDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (UserIsNotLoggedInException e)
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
