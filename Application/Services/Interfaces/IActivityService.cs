using Microsoft.AspNetCore.Identity;
using MyApi.Domain.Entities;

namespace MyApi.Application.Services.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityEntity>> GetAllActivitiesAsync();
        Task<IEnumerable<ActivityEntity>> GetAllActivitiesNoneArchivedAsync();
        Task<ActivityEntity?> GetActivityByIdAsync(int id);
        Task AddActivityAsync(ActivityEntity activity, int organizerId);
        Task<bool> UpdateActivityAsync(ActivityEntity activity, int organizerId, string role);
        Task<bool> ArchiveActivityAsync(int id, string role);
        Task<bool> DeleteActivityAsync(int id, int organizerId, string role);
    }
}
