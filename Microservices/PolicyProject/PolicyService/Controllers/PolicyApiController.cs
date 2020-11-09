using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PolicyService.Models;

namespace PolicyService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PolicyApiController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IPolicyRepository _policyRepository;

        public PolicyApiController(IPolicyRepository policyRepository, ILogger logger)
        {
            _policyRepository = policyRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetPolicies")]
        [ProducesResponseType(typeof(IEnumerable<Policy>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Policy>>> GetPolicies()
        {
            try
            {
                var result = await _policyRepository.GetPolicy();

                if (result != null && result.Any())
                    return Ok(result);

                _logger.Log(LogLevel.Warning, "Empty policy repository");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetPolicies - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPolicy/{policyId}")]
        [ProducesResponseType(typeof(Policy), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Policy>>> GetPolicy([FromRoute] long policyId)
        {
            try
            {
                var result = await _policyRepository.GetPolicy(policyId);

                if (result != null && result.Any())
                    return Ok(result.FirstOrDefault());

                _logger.Log(LogLevel.Warning, $"Couldn't find policy with id {policyId}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetPolicies - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDevicePlatforms")]
        [ProducesResponseType(typeof(IEnumerable<DevicePlatform>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<DevicePlatform>>> GetDevicePlatforms()
        {
            try
            {
                var result = await _policyRepository.GetDevicePlatform();

                if (result != null && result.Any())
                    return Ok(result);

                _logger.Log(LogLevel.Warning, "Empty policy repository");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetDevicePlatforms - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDevicePlatform")]
        [ProducesResponseType(typeof(DevicePlatform), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<DevicePlatform>>> GetDevicePlatform(
            [FromRoute] short devicePlatformId)
        {
            try
            {
                var result = await _policyRepository.GetDevicePlatform(devicePlatformId);

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

        [HttpPost]
        [Route("AddPolicy")]
        [ProducesResponseType(typeof(Policy), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Policy>> AddPolicy([FromBody] Policy newPolicy)
        {
            try
            {
                var result = await _policyRepository.AddPolicy(newPolicy);

                if (result != null)
                    return Ok(result);

                _logger.Log(LogLevel.Warning, $"Couldn't add policy {newPolicy.PolicyName}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call AddPolicy - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdatePolicy")]
        [ProducesResponseType(typeof(Policy), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Policy>> UpdatePolicy([FromBody] Policy policy)
        {
            try
            {
                var result = await _policyRepository.UpdatePolicy(policy);

                if (result)
                    return Ok(policy);

                _logger.Log(LogLevel.Warning, $"Couldn't update policy {policy.PolicyName}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call UpdatePolicy - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeletePolicy")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeletePolicy([FromRoute] int policyId)
        {
            try
            {
                var result = await _policyRepository.DeletePolicy(policyId);

                if (result)
                    return Ok();

                _logger.Log(LogLevel.Warning, $"Couldn't delete policy with id {policyId}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call DeletePolicy - {ex}");
                return BadRequest(ex.Message);
            }
        }
    }
}
