using Microsoft.EntityFrameworkCore;
using TrainingTrackerApi.Data;
using TrainingTrackerApi.Models;

namespace TrainingTrackerApi.Services;

public class TrainingDayService : ITrainingDayService
{
    private readonly AppDbContext _db;

    public TrainingDayService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<TrainingDay>> GetAllAsync(int? trainingWeekId, string? search)
    {
        var q = _db.TrainingDays.AsQueryable();

        if (trainingWeekId.HasValue)
            q = q.Where(d => d.TrainingWeekId == trainingWeekId.Value);

        if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(d => d.Title.Contains(search));

        return await q
            .OrderByDescending(d => d.Date)
            .ToListAsync();
    }

    public async Task<TrainingDay?> GetByIdAsync(int id)
    {
        return await _db.TrainingDays
            .Include(d => d.LiftEntries)
            .Include(d => d.NutritionEntries)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<TrainingDay?> CreateAsync(TrainingDay day)
    {
        var weekExists = await _db.TrainingWeeks.AnyAsync(w => w.Id == day.TrainingWeekId);
        if (!weekExists)
            return null;

        _db.TrainingDays.Add(day);
        await _db.SaveChangesAsync();
        return day;
    }

    public async Task<TrainingDayUpdateResult> UpdateAsync(int id, TrainingDay updated)
    {
        var existing = await _db.TrainingDays.FirstOrDefaultAsync(d => d.Id == id);
        if (existing is null) return TrainingDayUpdateResult.NotFound;

        var weekExists = await _db.TrainingWeeks.AnyAsync(w => w.Id == updated.TrainingWeekId);
        if (!weekExists) return TrainingDayUpdateResult.InvalidTrainingWeek;

        existing.Title = updated.Title;
        existing.Description = updated.Description;
        existing.Date = updated.Date;
        existing.TrainingWeekId = updated.TrainingWeekId;

        await _db.SaveChangesAsync();
        return TrainingDayUpdateResult.Updated;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.TrainingDays.FirstOrDefaultAsync(d => d.Id == id);
        if (existing is null) return false;

        _db.TrainingDays.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
