using Microsoft.EntityFrameworkCore;
using TrainingTrackerApi.Models;

namespace TrainingTrackerApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TrainingWeek> TrainingWeeks => Set<TrainingWeek>();
    public DbSet<LiftEntry> LiftEntries => Set<LiftEntry>();
    public DbSet<NutritionEntry> NutritionEntries => Set<NutritionEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrainingWeek>()
            .HasMany(w => w.LiftEntries)
            .WithOne(e => e.TrainingWeek)
            .HasForeignKey(e => e.TrainingWeekId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TrainingWeek>()
            .HasMany(w => w.NutritionEntries)
            .WithOne(e => e.TrainingWeek)
            .HasForeignKey(e => e.TrainingWeekId)
            .OnDelete(DeleteBehavior.Cascade);
    }
