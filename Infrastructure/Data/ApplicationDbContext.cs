using Microsoft.EntityFrameworkCore;
using MyApi.Domain.Entities;

namespace MyApi.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public required DbSet<ActivityEntity> Activities { get; set; }
        public required DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActivityEntity>()
                .HasMany(a => a.Organizers)
                .WithMany(u => u.OrganizedActivities)
                .UsingEntity(j => j.ToTable("ActivityOrganizers"));

             modelBuilder.Entity<ActivityEntity>()
                .HasMany(a => a.Registereds)
                .WithMany(u => u.RegisteredActivities)
                .UsingEntity(j => j.ToTable("ActivityRegistrations"));
        }
    }
}
