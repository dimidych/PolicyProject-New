using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GroupService;
using LoginService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PolicyService;
using PolicyService.Models;
using PolicySetService.Models;

namespace PolicySetService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PolicySetApiController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IPolicySetRepository _policySetRepository;
        private readonly IPolicyRepository _policyRepository;
        private readonly ILogger<PolicySetApiController> _logger;

        public PolicySetApiController(ILoginRepository loginRepository, IGroupRepository groupRepository,
            IPolicyRepository policyRepository, IPolicySetRepository policySetRepository,
            ILogger<PolicySetApiController> logger)
        {
            _loginRepository = loginRepository;
            _groupRepository = groupRepository;
            _policyRepository = policyRepository;
            _policySetRepository = policySetRepository;
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

        [HttpGet]
        [Route("GetPolicies")]
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
        [Route("GetPolicy?{policyId}")]
        public async Task<ActionResult<IEnumerable<Policy>>> GetPolicy([FromRoute] Guid policyId)
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
        [Route("GetPolicySets")]
        public async Task<ActionResult<IEnumerable<PolicySet>>> GetPolicySets()
        {
            try
            {
                var result = await _policySetRepository.GetPolicySets();

                if (result != null && result.Any())
                    return Ok(result);

                _logger.Log(LogLevel.Warning, "Empty policy set repository");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetPolicySets - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPolicySet?{policySet}")]
        public async Task<ActionResult<IEnumerable<PolicySet>>> GetPolicySet([FromRoute] PolicySet policySet)
        {
            try
            {
                var result = await _policySetRepository.GetPolicySets(policySet);

                if (result != null && result.Any())
                    return Ok(result.FirstOrDefault());

                _logger.Log(LogLevel.Warning, $"Couldn't find policy set");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetPolicySet - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPolicySetsForGroup?{groupId}")]
        public async Task<ActionResult<IEnumerable<PolicySet>>> GetPolicySetsForGroup([FromRoute] Guid groupId)
        {
            try
            {
                var result = await _policySetRepository.GetPolicySetsForGroup(groupId);

                if (result != null && result.Any())
                    return Ok(result);

                _logger.Log(LogLevel.Warning, $"Empty policy set list for group {groupId}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetPolicySetsForGroup - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPolicySetsForLogin?{loginId}")]
        public async Task<ActionResult<IEnumerable<PolicySet>>> GetPolicySetsForLogin([FromRoute] Guid loginId)
        {
            try
            {
                var result = await _policySetRepository.GetPolicySetsForLogin(loginId);

                if (result != null && result.Any())
                    return Ok(result);

                _logger.Log(LogLevel.Warning, $"Empty policy set list for login {loginId}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetPolicySetsForLogin - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetGroupIdAndPolicyIdDct")]
        public async Task<ActionResult<Dictionary<Guid, Guid[]>>> GetGroupIdAndPolicyIdDct()
        {
            try
            {
                var result = await _policySetRepository.GetGroupIdAndPolicyIdDct();

                if (result != null && result.Any())
                    return Ok(result);

                _logger.Log(LogLevel.Warning, "Empty policy ids list for groups");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetGroupIdAndPolicyIdDct - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddPolicySet")]
        public async Task<ActionResult<PolicySet>> AddPolicySet([FromBody] PolicySet newPolicySet)
        {
            try
            {
                var result = await _policySetRepository.AddPolicySet(newPolicySet);

                if (result != null)
                    return Ok(result);

                _logger.Log(LogLevel.Warning, $"Couldn't add policy set with params {newPolicySet.PolicyParam}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call AddPolicySet - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdatePolicySet")]
        public async Task<ActionResult<PolicySet>> UpdatePolicySet([FromBody] PolicySet policySet)
        {
            try
            {
                var result = await _policySetRepository.UpdatePolicySet(policySet);

                if (result)
                    return Ok(policySet);

                _logger.Log(LogLevel.Warning, $"Couldn't update policy Set {policySet.PolicySetId}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call UpdatePolicySet - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeletePolicySet")]
        public async Task<IActionResult> DeletePolicySet([FromRoute] Guid policySetId)
        {
            try
            {
                var result = await _policySetRepository.DeletePolicySet(policySetId);

                if (result)
                    return Ok();

                _logger.Log(LogLevel.Warning, $"Couldn't delete policy set with id {policySetId}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call DeletePolicySet - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdatePolicySetsForGroup")]
        public async Task<IActionResult> UpdatePolicySetsForGroup([FromBody] Guid groupId,
            [FromBody] PolicySet[] policySetList)
        {
            try
            {
                var result = await _policySetRepository.UpdatePolicySetsForGroup(groupId, policySetList);

                if (result)
                    return Ok();

                _logger.Log(LogLevel.Warning, $"Couldn't update policies for group {groupId}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call UpdatePolicySetsForGroup - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdatePolicySetsForLogin")]
        public async Task<IActionResult> UpdatePolicySetsForLogin([FromBody] Guid loginId,
            [FromBody] PolicySet[] policySetList)
        {
            try
            {
                var result = await _policySetRepository.UpdatePolicySetsForLogin(loginId, policySetList);

                if (result)
                    return Ok();

                _logger.Log(LogLevel.Warning, $"Couldn't update policies for login {loginId}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call UpdatePolicySetsForLogin - {ex}");
                return BadRequest(ex.Message);
            }
        }
    }
}
