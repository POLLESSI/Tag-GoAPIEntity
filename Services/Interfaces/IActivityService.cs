using MyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApi.Services
{
    public interface IActivityService
    {
        Task<IEnumerable<Activity>> GetAllActivitiesAsync();
        Task<Activity?> GetActivityByIdAsync(int id);
        Task AddActivityAsync(Activity activity);
        Task<bool> UpdateActivityAsync(Activity activity);
        Task<bool> DeleteActivityAsync(int id);
    }
}
