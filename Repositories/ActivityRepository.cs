using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Récupérer toutes les activités
        public async Task<IEnumerable<Activity>> GetAllActivitiesAsync()
        {
            return await _context.Activities
                .ToListAsync();
        }

        // Récupérer une activité par ID
        public async Task<Activity?> GetActivityByIdAsync(int id)
        {
            return await _context.Activities
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Ajouter une nouvelle activité
        public async Task AddActivityAsync(Activity activity)
        {
            await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();
        }

        // Mettre à jour une activité existante
        public async Task<bool> UpdateActivityAsync(Activity activity)
        {
            var existingActivity = await _context.Activities.FindAsync(activity.Id);

            if (existingActivity == null)
            {
                return false; // L'activité n'existe pas
            }

            existingActivity.Name = activity.Name;

            _context.Activities.Update(existingActivity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Supprimer une activité
        public async Task<bool> DeleteActivityAsync(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return false; // L'activité n'existe pas
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
