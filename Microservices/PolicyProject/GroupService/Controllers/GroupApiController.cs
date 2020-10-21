using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GroupService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GroupApiController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;

        public GroupApiController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Group>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            try
            {
                var result = await _groupRepository.GetGroup();

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
        [Route("{groupId}")]
        [ProducesResponseType(typeof(Group), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Group>> GetGroup([FromRoute] int groupId)
        {
            try
            {
                var result = await _groupRepository.GetGroup(groupId);

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
        [ProducesResponseType(typeof(Group), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Group>> AddGroup([FromBody] Group newGroup)
        {
            try
            {
                var result = await _groupRepository.AddGroup(newGroup);

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
        [ProducesResponseType(typeof(Group), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Group>> UpdateGroup([FromBody] Group group)
        {
            try
            {
                var result = await _groupRepository.UpdateGroup(group);

                if (!result)
                    return StatusCode(500);

                return Ok(group);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Group), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteGroup([FromRoute] int groupId)
        {
            try
            {
                var result = await _groupRepository.DeleteGroup(groupId);

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
