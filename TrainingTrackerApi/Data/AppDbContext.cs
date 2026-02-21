using Microsoft.EntityFrameworkCore;
using TrainingTrackerApi.Models;

namespace TrainingTrackerApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TrainingWeek> TrainingWeeks => Set<TrainingWeek>();
    public DbSet<TrainingDay> TrainingDays => Set<TrainingDay>();
    public DbSet<LiftEntry> LiftEntries => Set<LiftEntry>();
    public DbSet<NutritionEntry> NutritionEntries => Set<NutritionEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrainingWeek>()
            .HasMany(w => w.TrainingDays)
            .WithOne(d => d.TrainingWeek)
            .HasForeignKey(d => d.TrainingWeekId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TrainingDay>()
            .HasMany(d => d.LiftEntries)
            .WithOne(e => e.TrainingDay)
            .HasForeignKey(e => e.TrainingDayId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TrainingDay>()
            .HasMany(d => d.NutritionEntries)
            .WithOne(e => e.TrainingDay)
            .HasForeignKey(e => e.TrainingDayId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
