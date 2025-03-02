using MyApi.Domain.Entities;

namespace MyApi.Application.Services.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityEntity>> GetAllActivitiesAsync();
        Task<IEnumerable<ActivityEntity>> GetAllActivitiesNoneArchivedAsync();
        Task<ActivityEntity?> GetActivityByIdAsync(int id);
        Task AddActivityAsync(ActivityEntity activity);
        Task<bool> UpdateActivityAsync(ActivityEntity activity);
        Task<bool> DeleteActivityAsync(int id);
    }
}
