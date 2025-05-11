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

        public ActivityService(IActivityRepository activityRepository, IUserRepository userRepository)
        {
            _activityRepository = activityRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<ActivityEntity>> GetAllActivitiesAsync()
        {
            // Logique métier éventuelle avant d'appeler le repository
            return await _activityRepository.GetAllActivitiesAsync();
        }

        public async Task<IEnumerable<ActivityEntity>> GetAllActivitiesNoneArchivedAsync()
        {
            // Logique métier éventuelle avant d'appeler le repository
            return await _activityRepository.GetAllActivitiesNoneArchivedAsync();
        }

        public async Task<ActivityEntity?> GetActivityByIdAsync(int id)
        {
            // Logique métier pour vérifier des conditions avant la récupération
            return await _activityRepository.GetActivityByIdAsync(id);
        }

        public async Task AddActivityAsync(ActivityEntity activity, int organizerId)
        {
            UserEntity? user = await _userRepository.GetUserByIdAsync(organizerId);
            
            if (user == null)
            {
                throw new ArgumentException($"User with ID {organizerId} not found.");               
            }
     
            activity.Organizers.Add(user);

            activity.Active = true;

            await _activityRepository.AddActivityAsync(activity);
        }

         public async Task<bool> UpdateActivityAsync(ActivityEntity activity, int organizerId, string role)
        {
            // Logique métier, comme vérifier si l'activité est modifiable
            ActivityEntity? activityTemp = await _activityRepository.GetActivityByIdAsync(activity.Id);
      
            if (activity == null)
            {
                throw new ArgumentException($"Activity with ID {activity.Id} not found.");
            }

            UserEntity? user = activity.Organizers.FirstOrDefault(u => u.Id == organizerId);

            if (user == null && role != Roles.ADMIN)
            {
                throw new ArgumentException($"Role {role} is not authorized to update this activity.");
            }

            return await _activityRepository.UpdateActivityAsync(activity);
        }

        public async Task<bool> ArchiveActivityAsync(int id, string role)
        {
            // Logique métier, comme vérifier si l'activité est modifiable
            ActivityEntity? activity = await _activityRepository.GetActivityByIdAsync(id);
      
            if (activity == null)
            {
                throw new ArgumentException($"Activity with ID {id} not found.");
            }

            activity.Active = false;

            if (role != Roles.ADMIN)
            {
                throw new ArgumentException($"Role {role} is not authorized to update this activity.");
            }

            return await _activityRepository.UpdateActivityAsync(activity);
        }

        public async Task<bool> DeleteActivityAsync(int id, int organizerId, string role)
        {
            // Logique métier pour valider la suppression
            ActivityEntity? activity = await _activityRepository.GetActivityByIdAsync(id);

            if (activity == null)
            {
                throw new ArgumentException($"Activity with ID {id} not found.");
            }

            UserEntity? user = activity.Organizers.FirstOrDefault(u => u.Id == organizerId);

            if (user == null && role != Roles.ADMIN)
            {
                throw new ArgumentException($"Organizer with ID {organizerId} not found in activity.");
            }

            return await _activityRepository.DeleteActivityAsync(id);
        }
    }
}
