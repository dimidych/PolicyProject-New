using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Models;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserApiController> _logger;

        public UserApiController(IUserRepository userRepository, ILogger<UserApiController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var result = await _userRepository.GetUser();

                if (result != null && result.Any())
                    return Ok(result);

                _logger.Log(LogLevel.Warning, "Empty user repository");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetUsers - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUser?{userId}")]
        public async Task<ActionResult<User>> GetUser([FromRoute] Guid userId)
        {
            try
            {
                var result = await _userRepository.GetUser(userId);

                if (result != null && result.Any())
                    return Ok(result.FirstOrDefault());

                _logger.Log(LogLevel.Warning, $"Couldn't find user with id {userId}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetUser - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult<User>> AddUser([FromBody] User newUser)
        {
            try
            {
                var result = await _userRepository.AddUser(newUser);

                if (result != null)
                    return Ok(result);

                _logger.Log(LogLevel.Warning, $"Couldn't add user {newUser.UserName}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call AddUser - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult<User>> UpdateUser([FromBody] User user)
        {
            try
            {
                var result = await _userRepository.UpdateUser(user);

                if (result)
                    return Ok(user);

                _logger.Log(LogLevel.Warning, $"Couldn't update user {user.UserName}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call UpdateUser - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            try
            {
                var result = await _userRepository.DeleteUser(userId);

                if (result)
                    return Ok();

                _logger.Log(LogLevel.Warning, $"Couldn't delete user with id {userId}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call DeleteUser - {ex}");
                return BadRequest(ex.Message);
            }
        }
    }
}
