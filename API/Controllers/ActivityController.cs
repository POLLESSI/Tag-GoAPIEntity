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
            var activities = await _activityService.GetAllActivitiesAsync();
            
            // Utiliser AutoMapper pour transformer la liste d'entités en DTOs
            var activityDtos = _mapper.Map<IEnumerable<ActivityDto>>(activities);

            return Ok(activityDtos);
        }

        [HttpGet("ActivityActive")]
        [Authorize(Roles = $"{Roles.USER_LAMBDA}, {Roles.USER_ENTERPRISE}, {Roles.MODERATOR}, {Roles.ADMIN}")]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetAllActivitiesNoneArchived()
        {
            var activities = await _activityService.GetAllActivitiesNoneArchivedAsync();
            
            // Utiliser AutoMapper pour transformer la liste d'entités en DTOs
            var activityDtos = _mapper.Map<IEnumerable<ActivityDto>>(activities);

            return Ok(activityDtos);
        }

        // GET: api/ActivityEntity/5
        [HttpGet("{id}")]
        [Authorize(Roles = $"{Roles.MODERATOR}, {Roles.ADMIN}")]
        public async Task<ActionResult<ActivityDto>> GetActivity(int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            // Mapper l'entité ActivityEntity vers ActivityDto
            var activityDto = _mapper.Map<ActivityDto>(activity);

            return Ok(activityDto);
        }

        // POST: api/ActivityEntity
        [HttpPost]
        [Authorize(Roles = $"{Roles.MODERATOR}, {Roles.ADMIN}, {Roles.USER_ENTERPRISE}")]
        public async Task<ActionResult<ActivityDto>> CreateActivity(ActivityCreationDto activityDto)
        {
            int organizerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Mapper le DTO vers l'entité ActivityEntity
            var activity = _mapper.Map<ActivityEntity>(activityDto);

            await _activityService.AddActivityAsync(activity, organizerId);

            // Retourner l'entité créée sous forme de DTO
            var createdActivityDto = _mapper.Map<ActivityDto>(activity);

            return CreatedAtAction(nameof(GetActivity), new { id = activity.Id }, createdActivityDto);
        }

        // PUT: api/ActivityEntity/5
        [HttpPut("{id}")]
        [Authorize(Roles = $"{Roles.MODERATOR}, {Roles.ADMIN}, {Roles.USER_ENTERPRISE}")]
        public async Task<IActionResult> UpdateActivity(int id, ActivityEditionDto activityDto)
        {
            int organizerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            string role = User.FindFirst(ClaimTypes.Role)?.Value ?? "0";

            if (id != activityDto.Id)
            {
                return BadRequest();
            }

            // Mapper le DTO vers l'entité ActivityEntity à mettre à jour
            var activity = _mapper.Map<ActivityEntity>(activityDto);

            var updated = await _activityService.UpdateActivityAsync(activity, organizerId, role);

            if (!updated)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        // PUT: api/ActivityEntity/5
        [HttpPatch("patch/{id}")]
        [Authorize(Roles = $"{Roles.ADMIN}")]
        public async Task<IActionResult> ArchiveActivity(int id)
        {
            string role = User.FindFirst(ClaimTypes.Role)?.Value ?? "0";

            await _activityService.ArchiveActivityAsync(id, role);

            return Ok();
        }

        // DELETE: api/ActivityEntity/5
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{Roles.MODERATOR}, {Roles.ADMIN}, {Roles.USER_ENTERPRISE}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            int organizerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            string role = User.FindFirst(ClaimTypes.Role)?.Value ?? "0";

            var deleted = await _activityService.DeleteActivityAsync(id, organizerId, role);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
