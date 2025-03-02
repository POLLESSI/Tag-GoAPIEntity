
using MyApi.Domain.Entities;

namespace MyApi.Domain.Interfaces
{
    public interface IActivityRepository
    {
        Task<IEnumerable<ActivityEntity>> GetAllActivitiesAsync();
        Task<IEnumerable<ActivityEntity>> GetAllActivitiesNoneArchivedAsync();
        Task<ActivityEntity?> GetActivityByIdAsync(int id);
        Task AddActivityAsync(ActivityEntity activity);
        Task<bool> UpdateActivityAsync(ActivityEntity activity);
        Task<bool> DeleteActivityAsync(int id);
    }
}
