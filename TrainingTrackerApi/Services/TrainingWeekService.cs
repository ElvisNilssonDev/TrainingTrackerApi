using Microsoft.EntityFrameworkCore;
using TrainingTrackerApi.Data;
using TrainingTrackerApi.Models;

namespace TrainingTrackerApi.Services;

public class TrainingWeekService : ITrainingWeekService
{
    private readonly AppDbContext _db;

    public TrainingWeekService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<TrainingWeek>> GetAllAsync(string? search)
    {
        var q = _db.TrainingWeeks
            .Include(w => w.TrainingDays)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(w => w.Title.Contains(search));

        return await q
            .OrderByDescending(w => w.WeekStart)
            .ToListAsync();
    }

    public async Task<TrainingWeek?> GetByIdAsync(int id)
    {
        return await _db.TrainingWeeks
            .Include(w => w.TrainingDays)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<TrainingWeek> CreateAsync(TrainingWeek week)
    {
        _db.TrainingWeeks.Add(week);
        await _db.SaveChangesAsync();
        return week;
    }

    public async Task<bool> UpdateAsync(int id, TrainingWeek updated)
    {
        var existing = await _db.TrainingWeeks.FirstOrDefaultAsync(w => w.Id == id);
        if (existing is null) return false;

        existing.Title = updated.Title;
        existing.Description = updated.Description;
        existing.WeekStart = updated.WeekStart;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.TrainingWeeks.FirstOrDefaultAsync(w => w.Id == id);
        if (existing is null) return false;

        _db.TrainingWeeks.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
