using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using SportFixtures.Exceptions.UserExceptions;
using SportFixtures.Portal.DTOs;
using SportFixtures.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Controllers
{
    public class LoginController : ControllerBase
    {
        private static readonly string ACTION = "Login";

        private IUserBusinessLogic userBusinessLogic;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public LoginController(IUserBusinessLogic userBL, IMapper mapper, ILogger logger)
        {
            userBusinessLogic = userBL;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost]
        [Route("api/login")]
        public ActionResult Login([FromBody]LoginDTO data)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWrite(ACTION, "Model is invalid", $"Tried to login with: {data.Username}");
                return BadRequest(ModelState);
            }

            try
            {
                var loginSuccessful = userBusinessLogic.Login(mapper.Map<User>(data));
                var mappedUser = mapper.Map<UserDTO>(loginSuccessful);
                logger.LogWrite(ACTION, "Successful login!", mappedUser.Username);
                return Ok(mappedUser);
            }
            catch (UserDoesNotExistException e)
            {
                logger.LogWrite(ACTION, e.Message, $"Tried to login with: {data.Username}");
                return NotFound(e.Message);
            }
            catch (EmailOrPasswordException e)
            {
                logger.LogWrite(ACTION, e.Message, $"Tried to login with: {data.Username}");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                logger.LogWrite(ACTION, e.Message, $"Tried to login with: {data.Username}");
                return StatusCode(500, e.Message);
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
                return StatusCode(500, e.Message);
            }
        }
    }
}
