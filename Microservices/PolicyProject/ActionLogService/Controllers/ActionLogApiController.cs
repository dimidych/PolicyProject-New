﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ActionLogService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ActionLogService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ActionLogApiController : ControllerBase
    {
        private readonly IActionLogRepository _actionLogRepository;
        private readonly ILogger<ActionLogApiController> _logger;

        public ActionLogApiController(IActionLogRepository repository, ILogger<ActionLogApiController> logger)
        {
            _actionLogRepository = repository;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetEventActions")]
        public async Task<ActionResult<IEnumerable<EventAction>>> GetEventActions()
        {
            try
            {
                var result = await _actionLogRepository.GetEventAction();

                if (result != null && result.Any())
                    return Ok(result);

                _logger.Log(LogLevel.Warning, "Empty action log repository");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetEventActions - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetEventAction?{eventActionId}")]
        public async Task<ActionResult<EventAction>> GetEventAction([FromRoute] int eventActionId)
        {
            try
            {
                var result = await _actionLogRepository.GetEventAction(eventActionId);

                if (result != null && result.Any())
                    return Ok(result.FirstOrDefault());

                _logger.Log(LogLevel.Warning, $"Couldn't find event action with id {eventActionId}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetEventAction - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddEventAction")]
        public async Task<ActionResult<EventAction>> AddEventAction([FromBody] EventAction newEventAction)
        {
            try
            {
                var result = await _actionLogRepository.AddEventAction(newEventAction);

                if (result != null)
                    return Ok(result);

                _logger.Log(LogLevel.Warning, $"Couldn't add event action {newEventAction.EventName}");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call AddEventAction - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetActionLog")]
        public async Task<ActionResult<IEnumerable<ActionLog>>> GetActionLog([FromRoute] DateTime fromDate,
            [FromRoute] DateTime? toDate,
            [FromRoute] string action, [FromRoute] string login, [FromRoute] string deviceSerialNumber,
            [FromRoute] Guid? documentId, [FromRoute] string messageFilter)
        {
            try
            {
                var result = await _actionLogRepository.GetActionLog(fromDate, toDate, action, login,
                    deviceSerialNumber, documentId, messageFilter);

                if (result != null && result.Any())
                    return Ok(result);

                _logger.Log(LogLevel.Warning, "Empty action log repository");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call GetActionLog - {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddActionLog")]
        public async Task<ActionResult<ActionLog>> AddActionLog([FromBody] ActionLog newActionLog)
        {
            try
            {
                var result = await _actionLogRepository.AddActionLog(newActionLog);

                if (result != null)
                    return Ok(result);

                _logger.Log(LogLevel.Warning, "Couldn't add action log");
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while call AddActionLog - {ex}");
                return BadRequest(ex.Message);
            }
        }
    }
}
