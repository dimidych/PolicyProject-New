using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserApiController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var result = await _userRepository.GetUser();

                if (result == null || !result.Any())
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(typeof(User), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<User>> GetUser([FromRoute] int userId)
        {
            try
            {
                var result = await _userRepository.GetUser(userId);

                if (result == null || !result.Any())
                    return NotFound();

                return Ok(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<User>> AddUser([FromBody] User newUser)
        {
            try
            {
                var result = await _userRepository.AddUser(newUser);

                if (result == null)
                    return StatusCode(500);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(User), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<User>> UpdateUser([FromBody] User user)
        {
            try
            {
                var result = await _userRepository.UpdateUser(user);

                if (!result)
                    return StatusCode(500);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(User), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            try
            {
                var result = await _userRepository.DeleteUser(userId);

                if (!result)
                    return StatusCode(500);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
