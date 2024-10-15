using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MyApi.Models;
using MyApi.Services;
using MyApi.DTOs;

using AutoMapper;


namespace MyApi.Controllers
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

        // GET: api/Activity
        [HttpGet]
        // [Authorize]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivities()
        {
            var activities = await _activityService.GetAllActivitiesAsync();
            
            // Utiliser AutoMapper pour transformer la liste d'entités en DTOs
            var activityDtos = _mapper.Map<IEnumerable<ActivityDto>>(activities);

            return Ok(activityDtos);
        }

        // GET: api/Activity/5
        [HttpGet("{id}")]
        // [Authorize]
        public async Task<ActionResult<ActivityDto>> GetActivity(int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            // Mapper l'entité Activity vers ActivityDto
            var activityDto = _mapper.Map<ActivityDto>(activity);

            return Ok(activityDto);
        }

        // POST: api/Activity
        [HttpPost]
        // [Authorize]
        public async Task<ActionResult<ActivityDto>> CreateActivity(ActivityCreationDto activityDto)
        {
            // Mapper le DTO vers l'entité Activity
            var activity = _mapper.Map<Activity>(activityDto);

            await _activityService.AddActivityAsync(activity);

            // Retourner l'entité créée sous forme de DTO
            var createdActivityDto = _mapper.Map<ActivityDto>(activity);

            return CreatedAtAction(nameof(GetActivity), new { id = activity.Id }, createdActivityDto);
        }

        // PUT: api/Activity/5
        [HttpPut("{id}")]
        // [Authorize]
        public async Task<IActionResult> UpdateActivity(int id, ActivityEditionDto activityDto)
        {
            if (id != activityDto.Id)
            {
                return BadRequest();
            }

            // Mapper le DTO vers l'entité Activity à mettre à jour
            var activity = _mapper.Map<Activity>(activityDto);

            var updated = await _activityService.UpdateActivityAsync(activity);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Activity/5
        [HttpDelete("{id}")]
        // [Authorize]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var deleted = await _activityService.DeleteActivityAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
