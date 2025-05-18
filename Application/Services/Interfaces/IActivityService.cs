using MyApi.Application.DTOs.ActivityDTOs;
using MyApi.Domain.Entities;

namespace MyApi.Application.Services.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityDto>> GetAllActivitiesAsync();
        Task<IEnumerable<ActivityDto>> GetAllActivitiesNoneArchivedAsync();
        Task<ActivityDto?> GetActivityByIdAsync(int id);
        Task<ActivityDto> AddActivityAsync(ActivityCreationDto activityCreationDto, int organizerId);
        Task<bool> UpdateActivityAsync(ActivityEditionDto activityEditionDto, int organizerId, string role);
        Task<bool> ArchiveActivityAsync(int id, string role);
        Task<bool> DeleteActivityAsync(int id, int organizerId, string role);
    }
}
