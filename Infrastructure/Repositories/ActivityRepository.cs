using MyApi.Domain.Entities;
using MyApi.Domain.Interfaces;
using MyApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace MyApi.Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Récupérer toutes les activités
        public async Task<IEnumerable<ActivityEntity>> GetAllActivitiesAsync()
        {
            return await _context.Activities
                .ToListAsync();
        }

        // Récupérer toutes les activités
        public async Task<IEnumerable<ActivityEntity>> GetAllActivitiesNoneArchivedAsync()
        {
            return await _context.Activities
                .Where(activity => activity.Active) // Filtre les activités non archivées
                .Include(activity => activity.Organizers)
                .ToListAsync();
        }

        // Récupérer une activité par ID
        public async Task<ActivityEntity?> GetActivityByIdAsync(int id)
        {
            return await _context.Activities
                .Include(activity => activity.Organizers)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Ajouter une nouvelle activité
        public async Task AddActivityAsync(ActivityEntity activity)
        {
            await _context.Activities
                .AddAsync(activity);

            await _context
                .SaveChangesAsync();
        }

        // Mettre à jour une activité existante
        public async Task<bool> UpdateActivityAsync(ActivityEntity activity)
        {
            var existing = await _context.Activities
                .Include(a => a.Organizers)
                .Include(a => a.Registereds)
                .FirstOrDefaultAsync(a => a.Id == activity.Id);

            if (existing == null)
                return false;

            // Update des propriétés simples
            _context.Entry(existing).CurrentValues.SetValues(activity);

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
