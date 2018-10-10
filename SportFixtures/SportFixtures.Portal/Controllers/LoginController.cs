using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Exceptions.UserExceptions;
using SportFixtures.Portal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Controllers
{
    public class LoginController : ControllerBase
    {
        private IUserBusinessLogic userBusinessLogic;
        private readonly IMapper mapper;

        public LoginController(IUserBusinessLogic userBL, IMapper mapper)
        {
            userBusinessLogic = userBL;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("api/login")]
        public ActionResult Login([FromBody]LoginDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var loginSuccessful = userBusinessLogic.Login(mapper.Map<User>(data));
                return Ok(loginSuccessful);
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
