using MyApi.Models;

namespace MyApi.Repositories
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> GetAllActivitiesAsync();
        Task<Activity?> GetActivityByIdAsync(int id);
        Task AddActivityAsync(Activity activity);
        Task<bool> UpdateActivityAsync(Activity activity);
        Task<bool> DeleteActivityAsync(int id);
    }
}
