using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LoginService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoginService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoginApiController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ILogger _logger;

        public LoginApiController(ILoginRepository loginRepository, ILogger logger)
        {
            _loginRepository = loginRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetGroups")]
        [ProducesResponseType(typeof(IEnumerable<Group>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            try
            {
                var result = await _loginRepository.GetGroup();

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
        [Route("GetGroup/{groupId}")]
        [ProducesResponseType(typeof(Group), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Group>> GetGroup([FromRoute] int groupId)
        {
            try
            {
                var result = await _loginRepository.GetGroup(groupId);

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
        [ProducesResponseType(typeof(IEnumerable<User>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var result = await _loginRepository.GetUser();

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
        [Route("GetUser/{userId}")]
        [ProducesResponseType(typeof(User), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<User>> GetUser([FromRoute] int userId)
        {
            try
            {
                var result = await _loginRepository.GetUser(userId);

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
        [ProducesResponseType(typeof(IEnumerable<Login>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
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
        [Route("GetLogin/{login}")]
        [ProducesResponseType(typeof(Login), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
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
        [ProducesResponseType(typeof(Login), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
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
        [ProducesResponseType(typeof(Login), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
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
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteLogin([FromRoute] int loginId)
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
