using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DeviceService;
using LoginDevicesService.Models;
using LoginService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoginDevicesService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoginDevicesApiController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly ILoginDevicesRepository _loginDevicesRepository;
        private readonly ILogger<LoginDevicesApiController> _logger;

        public LoginDevicesApiController(ILoginDevicesRepository loginDevicesRepository,
            ILoginRepository loginRepository, IDeviceRepository deviceRepository,
            ILogger<LoginDevicesApiController> logger)
        {
            _loginDevicesRepository = loginDevicesRepository;
            _loginRepository = loginRepository;
            _deviceRepository = deviceRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetDevicesOfLogin?{login}")]
        public async Task<ActionResult<IEnumerable<LoginDevices>>> GetDevicesOfLogin([FromRoute] string login)
        {
            try
            {
                var result = await _loginDevicesRepository.GetDevicesOfLogin(login);

                if (result != null && result.Any())
                    return Ok(result);

                _logger.Log(LogLevel.Warning, $"Couldn't find devices for login {login}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetDevicesOfLogin - {ex}");
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
        [Route("GetDevices")]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            try
            {
                var result = await _deviceRepository.GetDevice();

                if (result != null && result.Any())
                    return Ok(result);

                _logger.Log(LogLevel.Warning, "Empty device repository");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetDevices - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetLoginIdAndDeviceIdDct")]
        public async Task<ActionResult<Dictionary<Guid, Guid[]>>> GetLoginIdAndDeviceIdDct()
        {
            try
            {
                var result = await _loginDevicesRepository.GetLoginIdAndDeviceIdDct();

                if (result != null && result.Any())
                    return Ok(result);

                _logger.Log(LogLevel.Warning, "Empty Login-Device repository");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetLoginIdAndDeviceIdDct - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateLoginDevices")]
        public async Task<ActionResult> UpdateLoginDevices([FromBody] Guid loginId,
            [FromBody] LoginDevices[] loginDevicesList)
        {
            try
            {
                var result = await _loginDevicesRepository.UpdateLoginDevices(loginId, loginDevicesList);

                if (result)
                    return Ok();

                _logger.Log(LogLevel.Warning, $"Couldn't update devices for login with id {loginId}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call UpdateLoginDevices - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("SetDevicesForUpdate")]
        public async Task<ActionResult> SetDevicesForUpdate(List<Guid> loginIdList)
        {
            try
            {
                var result = await _loginDevicesRepository.SetDevicesForUpdate(loginIdList);

                if (result)
                    return Ok();

                _logger.Log(LogLevel.Warning, "Couldn't update devices");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call SetDevicesForUpdate - {ex}");
                return BadRequest(ex.Message);
            }
        }
    }
}
