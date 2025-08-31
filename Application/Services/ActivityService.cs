using AutoMapper;
using MyApi.Application.DTOs.ActivityDTOs;
using MyApi.Application.Services.Interfaces;
using MyApi.Constants;
using MyApi.Domain.Entities;
using MyApi.Domain.Interfaces;
using System.Security.Claims;

namespace MyApi.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ActivityService(IActivityRepository activityRepository, IUserRepository userRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActivityDto>> GetAllActivitiesAsync()
        {
            IEnumerable<ActivityEntity> activities =  await _activityRepository.GetAllActivitiesAsync();
            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        public async Task<IEnumerable<ActivityDto>> GetAllActivitiesNoneArchivedAsync()
        {
            IEnumerable<ActivityEntity> activities =  await _activityRepository.GetAllActivitiesNoneArchivedAsync();
            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        public async Task<ActivityDto?> GetActivityByIdAsync(int id)
        {
            ActivityEntity? activity = await _activityRepository.GetActivityByIdAsync(id);
            return _mapper.Map<ActivityDto>(activity);
        }

        public async Task<ActivityDto> AddActivityAsync(ActivityCreationDto activityCreationDto, int organizerId)
        {
            ActivityEntity activity = _mapper.Map<ActivityEntity>(activityCreationDto);

            UserEntity? user = await _userRepository.GetUserByIdAsync(organizerId);
            
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {organizerId} not found.");               
            }
     
            activity.Organizers.Add(user);

            activity.Active = true;

            await _activityRepository.AddActivityAsync(activity);

            return _mapper.Map<ActivityDto>(activity);
        }

         public async Task<bool> UpdateActivityAsync(ActivityEditionDto activityEditionDto, int organizerId, string role)
        {
            ActivityEntity? activityTemp = await _activityRepository.GetActivityByIdAsync(activityEditionDto.Id);
      
            if (activityTemp == null)
            {
                throw new KeyNotFoundException($"Activity with ID {activityTemp.Id} not found.");
            }

            UserEntity? user = activityTemp.Organizers.FirstOrDefault(u => u.Id == organizerId);

            if (user == null && role != Roles.ADMIN)
            {
                throw new UnauthorizedAccessException($"Role {role} is not authorized to update this activity.");
            }

            ActivityEntity activity = _mapper.Map<ActivityEntity>(activityEditionDto);

            activity.Active = true;

            bool success = await _activityRepository.UpdateActivityAsync(activity);
            if (!success)
            {
                throw new InvalidOperationException("Failed to update the activity. Please try again.");
            }

            return true;
        }

        public async Task<bool> ArchiveActivityAsync(int id, string role)
        {
            // Logique métier, comme vérifier si l'activité est modifiable
            ActivityEntity? activity = await _activityRepository.GetActivityByIdAsync(id);
      
            if (activity == null)
            {
                throw new KeyNotFoundException($"Activity with ID {id} not found.");
            }

            if (role != Roles.ADMIN)
            {
                throw new UnauthorizedAccessException($"Role {role} is not authorized to update this activity.");
            }

            activity.Active = false;

            bool success = await _activityRepository.UpdateActivityAsync(activity);
            if (!success)
            {
                throw new InvalidOperationException("Failed to delete the activity. Please try again.");
            }

            return true;
        }

        public async Task<bool> DeleteActivityAsync(int id, int organizerId, string role)
        {
            // Vérifier l'existence de l'activité
            ActivityEntity? activity = await _activityRepository.GetActivityByIdAsync(id);
            if (activity == null)
            {
                throw new KeyNotFoundException($"Activity with ID {id} not found.");
            }

            // Vérifier si l'utilisateur est organisateur ou admin
            UserEntity? user = activity.Organizers.FirstOrDefault(u => u.Id == organizerId);
            if (user == null && role != Roles.ADMIN)
            {
                throw new UnauthorizedAccessException($"You do not have permission to delete this activity.");
            }

            // Suppression de l'activité
            bool success = await _activityRepository.DeleteActivityAsync(id);
            if (!success)
            {
                throw new InvalidOperationException("Failed to delete the activity. Please try again.");
            }

            return true;
        }
    }
}
