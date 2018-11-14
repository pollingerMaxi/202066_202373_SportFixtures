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
                var mappedUser = mapper.Map<UserDTO>(loginSuccessful);
                return Ok(mappedUser);
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
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("api/logout")]
        public ActionResult Logout([FromBody]LogoutDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                userBusinessLogic.Logout(data.Username);
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
                return StatusCode(500, e.Message);
            }
        }
    }
}
