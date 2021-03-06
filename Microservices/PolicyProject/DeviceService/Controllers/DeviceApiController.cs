﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DevicePlatformEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeviceService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DeviceApiController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDevicePlatformRepository _devicePlatformRepository;
        private readonly ILogger<DeviceApiController> _logger;

        public DeviceApiController(IDevicePlatformRepository devicePlatformRepository,
            IDeviceRepository deviceRepository,
            ILogger<DeviceApiController> logger)
        {
            _devicePlatformRepository = devicePlatformRepository;
            _deviceRepository = deviceRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetDevicePlatforms")]
        public async Task<ActionResult<IEnumerable<DevicePlatform>>> GetDevicePlatforms()
        {
            try
            {
                var result = await _devicePlatformRepository.GetDevicePlatform();

                if (result != null && result.Any())
                    return Ok(result);

                _logger.Log(LogLevel.Warning, "Empty device platform repository");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetDevicePlatforms - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDevicePlatform?{devicePlatformId}")]
        public async Task<ActionResult<DevicePlatform>> GetDevicePlatform([FromRoute] short devicePlatformId)
        {
            try
            {
                var result = await _devicePlatformRepository.GetDevicePlatform(devicePlatformId);

                if (result != null && result.Any())
                    return Ok(result.FirstOrDefault());

                _logger.Log(LogLevel.Warning, $"Couldn't find device platform with id {devicePlatformId}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetDevicePlatform - {ex}");
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
        [Route("GetDevice?{deviceId}")]
        public async Task<ActionResult<Device>> GetDevice([FromRoute] Guid deviceId)
        {
            try
            {
                var result = await _deviceRepository.GetDevice(deviceId);

                if (result != null && result.Any())
                    return Ok(result.FirstOrDefault());

                _logger.Log(LogLevel.Warning, $"Couldn't find device with id {deviceId}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetDevice - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddDevice")]
        public async Task<ActionResult<Device>> AddDevice([FromBody] Device newDevice)
        {
            try
            {
                var result = await _deviceRepository.AddDevice(newDevice);

                if (result != null)
                    return Ok(result);

                _logger.Log(LogLevel.Warning, $"Couldn't add device {newDevice.DeviceName}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call AddDevice - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddDevicePlatform")]
        public async Task<ActionResult<DevicePlatform>> AddDevicePlatform([FromBody] DevicePlatform newDevicePlatform)
        {
            try
            {
                var result = await _devicePlatformRepository.AddDevicePlatform(newDevicePlatform);

                if (result != null)
                    return Ok(result);

                _logger.Log(LogLevel.Warning, $"Couldn't add device platform {newDevicePlatform.DevicePlatformName}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call AddDevicePlatform - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateDevice")]
        public async Task<ActionResult<Device>> UpdateDevice([FromBody] Device device)
        {
            try
            {
                var result = await _deviceRepository.UpdateDevice(device);

                if (result)
                    return Ok(device);

                _logger.Log(LogLevel.Warning, $"Couldn't update device {device.DeviceName}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call UpdateDevice - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteDevice")]
        public async Task<IActionResult> DeleteDevice([FromRoute] Guid deviceId)
        {
            try
            {
                var result = await _deviceRepository.DeleteDevice(deviceId);

                if (result)
                    return Ok();

                _logger.Log(LogLevel.Warning, $"Couldn't delete device with id {deviceId}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call DeleteDevice - {ex}");
                return BadRequest(ex.Message);
            }
        }
    }
}
