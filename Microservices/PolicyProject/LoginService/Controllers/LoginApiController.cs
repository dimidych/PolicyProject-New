using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GroupService;
using LoginService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Models;

namespace LoginService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoginApiController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILoginRepository _loginRepository;
        private readonly ILogger<LoginApiController> _logger;

        public LoginApiController(IGroupRepository groupRepository, IUserRepository userRepository,
            ILoginRepository loginRepository, ILogger<LoginApiController> logger)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _loginRepository = loginRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetGroups")]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            try
            {
                var result = await _groupRepository.GetGroup();

                if (result != null && result.Any())
                    return Ok(result);

                _logger.Log(LogLevel.Warning, "Empty login repository");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetGroups - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetGroup?{groupId}")]
        public async Task<ActionResult<Group>> GetGroup([FromRoute] Guid groupId)
        {
            try
            {
                var result = await _groupRepository.GetGroup(groupId);

                if (result != null && result.Any())
                    return Ok(result.FirstOrDefault());

                _logger.Log(LogLevel.Warning, $"Couldn't find group with id {groupId}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetGroup - {ex}");
                return BadRequest(ex.Message);
            }
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

                _logger.Log(LogLevel.Warning, "Empty login repository");
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

                _logger.Log(LogLevel.Warning, $"Couldn't find User with id {userId}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetUser - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetLogins")]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogins()
        {
            try
            {
                var result = await _loginRepository.GetLogin();

                if (result != null && result.Any())
                    return Ok(result);

                _logger.Log(LogLevel.Warning, "Empty login repository");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetLogins - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetLogin?{login}")]
        public async Task<ActionResult<Login>> GetLogin([FromRoute] string login)
        {
            try
            {
                var result = await _loginRepository.GetLogin(login);

                if (result != null && result.Any())
                    return Ok(result.FirstOrDefault());

                _logger.Log(LogLevel.Warning, $"Couldn't find login {login}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetLogin - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddLogin")]
        public async Task<ActionResult<Login>> AddLogin([FromBody] Login newLogin)
        {
            try
            {
                var result = await _loginRepository.AddLogin(newLogin);

                if (result != null)
                    return Ok(result);

                _logger.Log(LogLevel.Warning, $"Couldn't add login {newLogin.LogIn}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call AddLogin - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateLogin")]
        public async Task<ActionResult<Login>> UpdateLogin([FromBody] Login login)
        {
            try
            {
                var result = await _loginRepository.UpdateLogin(login);

                if (result)
                    return Ok(login);

                _logger.Log(LogLevel.Warning, $"Couldn't update login {login.LogIn}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call UpdateLogin - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteLogin")]
        public async Task<IActionResult> DeleteLogin([FromRoute] Guid loginId)
        {
            try
            {
                var result = await _loginRepository.DeleteLogin(loginId);

                if (result)
                    return Ok();

                _logger.Log(LogLevel.Warning, $"Couldn't delete login with id {loginId}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call DeleteLogin - {ex}");
                return BadRequest(ex.Message);
            }
        }
    }
}
