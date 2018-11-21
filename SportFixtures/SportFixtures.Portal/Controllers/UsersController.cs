using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data;
using SportFixtures.Data.Entities;
using SportFixtures.Exceptions.TeamExceptions;
using SportFixtures.Exceptions.UserExceptions;
using SportFixtures.Portal.DTOs;
using SportFixtures.Portal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private IUserBusinessLogic userBusinessLogic;
        private readonly IMapper mapper;

        public UsersController(IUserBusinessLogic userBL, IMapper mapper)
        {
            userBusinessLogic = userBL;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ICollection<UserDTO>> GetAllUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var users = mapper.Map<UserDTO[]>(userBusinessLogic.GetAll());
                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = mapper.Map<UserDTO>(userBusinessLogic.GetById(id));
                return Ok(user);
            }
            catch (UserDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [AuthorizedRoles(Role.Admin)]
        public ActionResult CreateUser([FromBody]User data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                userBusinessLogic.AddUser(data);
                return Ok(mapper.Map<UserDTO>(data));
            }
            catch (UserException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [AuthorizedRoles(Role.Admin)]
        public ActionResult UpdateUser([FromBody]UserDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = mapper.Map<User>(data);
                userBusinessLogic.Update(user);
                return Ok(mapper.Map<UserDTO>(user));
            }
            catch (UserDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (LoggedUserIsNotAdminException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        [AuthorizedRoles(Role.Admin)]
        public ActionResult DeleteUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                userBusinessLogic.Delete(id);
                return Ok();
            }
            catch (UserDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("favorite")]
        public ActionResult FavoriteTeam([FromBody]UsersTeams userteam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                userBusinessLogic.FollowTeam(userteam.UserId, userteam.TeamId);
                return Ok();
            }
            catch (TeamDoesNotExistsException e)
            {
                return NotFound(e.Message);
            }
            catch (UserDoesNotExistException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return BadRequest("User is already following this team.");
            }
        }

        [HttpGet("favorites/{userId}")]
        public ActionResult GetFavorites([FromRoute]int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var favorites = userBusinessLogic.GetFavoritesOfUser(userId);
                return Ok(favorites);
            }
            catch (UserDoesNotExistException e)
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
