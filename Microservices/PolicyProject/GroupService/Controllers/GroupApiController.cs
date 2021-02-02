using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GroupService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GroupApiController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<GroupApiController> _logger;

        public GroupApiController(IGroupRepository groupRepository, ILogger<GroupApiController> logger)
        {
            _groupRepository = groupRepository;
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

                _logger.Log(LogLevel.Warning, "Empty group repository");
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
                _logger.Log(LogLevel.Error, $"Error while call GetUser - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddGroup")]
        public async Task<ActionResult<Group>> AddGroup([FromBody] Group newGroup)
        {
            try
            {
                var result = await _groupRepository.AddGroup(newGroup);

                if (result != null)
                    return Ok(result);

                _logger.Log(LogLevel.Warning, $"Couldn't add group {newGroup.GroupName}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call AddGroup - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateGroup")]
        public async Task<ActionResult<Group>> UpdateGroup([FromBody] Group group)
        {
            try
            {
                var result = await _groupRepository.UpdateGroup(group);

                if (result)
                    return Ok(@group);

                _logger.Log(LogLevel.Warning, $"Couldn't update group {@group.GroupName}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call UpdateGroup - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteGroup")]
        public async Task<IActionResult> DeleteGroup([FromRoute] Guid groupId)
        {
            try
            {
                var result = await _groupRepository.DeleteGroup(groupId);

                if (result)
                    return Ok();

                _logger.Log(LogLevel.Warning, $"Couldn't delete group with id {groupId}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call DeleteGroup - {ex}");
                return BadRequest(ex.Message);
            }
        }
    }
}
