using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using AutoMapper;
using MyApi.Application.DTOs.ActivityDTOs;
using MyApi.Application.Services.Interfaces;
using MyApi.Domain.Entities;
using MyApi.Constants;
using System.Security.Claims;

namespace MyApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly IMapper _mapper;

        public ActivityController(IActivityService activityService, IMapper mapper)
        {
            _activityService = activityService;
            _mapper = mapper;
        }

        // GET: api/ActivityEntity
        [HttpGet]
        [Authorize(Roles = $"{Roles.MODERATOR}, {Roles.ADMIN}")]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivities()
        {
            IEnumerable<ActivityDto> activityDtos = await _activityService.GetAllActivitiesAsync();

            if (activityDtos == null || !activityDtos.Any())
            {
                return NotFound();
            }

            return Ok(activityDtos);
        }

        [HttpGet("ActivityActive")]
        [Authorize(Roles = $"{Roles.USER_LAMBDA}, {Roles.USER_ENTERPRISE}, {Roles.MODERATOR}, {Roles.ADMIN}")]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetAllActivitiesNoneArchived()
        {
            IEnumerable<ActivityDto> activityDtos  = await _activityService.GetAllActivitiesNoneArchivedAsync();
            
            if (activityDtos == null || !activityDtos.Any())
            {
                return NotFound();
            }

            return Ok(activityDtos);
        }

        // GET: api/ActivityEntity/5
        [HttpGet("{id}")]
        [Authorize(Roles = $"{Roles.MODERATOR}, {Roles.ADMIN}")]
        public async Task<ActionResult<ActivityDto>> GetActivity(int id)
        {
            ActivityDto? activityDto = await _activityService.GetActivityByIdAsync(id);

            if (activityDto == null)
            {
                return NotFound();
            }

            return Ok(activityDto);
        }

        // POST: api/ActivityEntity
        [HttpPost]
        [Authorize(Roles = $"{Roles.MODERATOR}, {Roles.ADMIN}, {Roles.USER_ENTERPRISE}")]
        public async Task<ActionResult<ActivityDto>> CreateActivity(ActivityCreationDto activityCreationDto)
        {
            int organizerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            ActivityDto activityDto = await _activityService.AddActivityAsync(activityCreationDto, organizerId);

            if (activityDto == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetActivity), new { id = activityDto.Id }, activityDto);
        }

        // PUT: api/ActivityEntity/5
        [HttpPut("{id}")]
        [Authorize(Roles = $"{Roles.MODERATOR}, {Roles.ADMIN}, {Roles.USER_ENTERPRISE}")]
        public async Task<IActionResult> UpdateActivity(int id, ActivityEditionDto activityEditionDto)
        {
            int organizerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            string role = User.FindFirst(ClaimTypes.Role)?.Value ?? "0";

            try
            {
                await _activityService.UpdateActivityAsync(activityEditionDto, organizerId, role);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT: api/ActivityEntity/5
        [HttpPatch("ActivityArchive/{id}")]
        [Authorize(Roles = $"{Roles.ADMIN}")]
        public async Task<IActionResult> ArchiveActivity(int id)
        {
            string role = User.FindFirst(ClaimTypes.Role)?.Value ?? "0";

            try
            {
                await _activityService.ArchiveActivityAsync(id, role);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE: api/ActivityEntity/5
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{Roles.MODERATOR}, {Roles.ADMIN}, {Roles.USER_ENTERPRISE}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            int organizerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            string role = User.FindFirst(ClaimTypes.Role)?.Value ?? "0";

            try
            {
                await _activityService.DeleteActivityAsync(id, organizerId, role);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
