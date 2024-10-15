using MyApi.Models;
using MyApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApi.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<IEnumerable<Activity>> GetAllActivitiesAsync()
        {
            // Logique métier éventuelle avant d'appeler le repository
            return await _activityRepository.GetAllActivitiesAsync();
        }

        public async Task<Activity?> GetActivityByIdAsync(int id)
        {
            // Logique métier pour vérifier des conditions avant la récupération
            return await _activityRepository.GetActivityByIdAsync(id);
        }

        public async Task AddActivityAsync(Activity activity)
        {
            // Valider ou modifier l'activité avant l'ajout
            await _activityRepository.AddActivityAsync(activity);
        }

        public async Task<bool> UpdateActivityAsync(Activity activity)
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
