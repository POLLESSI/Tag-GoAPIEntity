using MyApi.Application.Services.Interfaces;
using MyApi.Domain.Entities;
using MyApi.Domain.Interfaces;

namespace MyApi.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
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

        public async Task AddActivityAsync(ActivityEntity activity)
        {
            // Valider ou modifier l'activité avant l'ajout

            activity.Active = true;

            await _activityRepository.AddActivityAsync(activity);
        }

        public async Task<bool> UpdateActivityAsync(ActivityEntity activity)
        {
            // Logique métier, comme vérifier si l'activité est modifiable
            return await _activityRepository.UpdateActivityAsync(activity);
        }

        public async Task<bool> DeleteActivityAsync(int id)
        {
            // Logique métier pour valider la suppression
            return await _activityRepository.DeleteActivityAsync(id);
        }
    }
}
