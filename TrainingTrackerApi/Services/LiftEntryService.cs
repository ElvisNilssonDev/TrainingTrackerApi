using Microsoft.EntityFrameworkCore;
using TrainingTrackerApi.Data;
using TrainingTrackerApi.Models;

namespace TrainingTrackerApi.Services;

public class LiftEntryService : ILiftEntryService
{
    private readonly AppDbContext _db;

    public LiftEntryService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<LiftEntry>> GetAllAsync(int? trainingWeekId, string? exercise)
    {
        var query = _db.LiftEntries.AsQueryable();

        if (trainingWeekId.HasValue)
        {
            query = query.Where(l => l.TrainingWeekId == trainingWeekId.Value);
        }

        if (!string.IsNullOrWhiteSpace(exercise))
        {
            query = query.Where(l => l.Exercise.Contains(exercise));
        }

        return await query
            .OrderByDescending(l => l.Time)
            .ToListAsync();
    }

    public async Task<LiftEntry?> GetByIdAsync(int id)
    {
        return await _db.LiftEntries.FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<LiftEntry?> CreateAsync(LiftEntry entry)
    {
        var weekExists = await _db.TrainingWeeks.AnyAsync(w => w.Id == entry.TrainingWeekId);
        if (!weekExists)
        {
            return null;
        }

        _db.LiftEntries.Add(entry);
        await _db.SaveChangesAsync();
        return entry;
    }

    public async Task<LiftEntryUpdateResult> UpdateAsync(int id, LiftEntry updated)
    {
        var existing = await _db.LiftEntries.FirstOrDefaultAsync(l => l.Id == id);
        if (existing is null)
        {
            return LiftEntryUpdateResult.NotFound;
        }

        var weekExists = await _db.TrainingWeeks.AnyAsync(w => w.Id == updated.TrainingWeekId);
        if (!weekExists)
        {
            return LiftEntryUpdateResult.InvalidTrainingWeek;
        }

        existing.Title = updated.Title;
        existing.Time = updated.Time;
        existing.Exercise = updated.Exercise;
        existing.WeightKg = updated.WeightKg;
        existing.Reps = updated.Reps;
        existing.Sets = updated.Sets;
        existing.TrainingWeekId = updated.TrainingWeekId;

        await _db.SaveChangesAsync();
        return LiftEntryUpdateResult.Updated;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.LiftEntries.FirstOrDefaultAsync(l => l.Id == id);
        if (existing is null)
        {
            return false;
        }

        _db.LiftEntries.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
