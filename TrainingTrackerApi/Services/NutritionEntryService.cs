using Microsoft.EntityFrameworkCore;
using TrainingTrackerApi.Data;
using TrainingTrackerApi.Models;

namespace TrainingTrackerApi.Services;

public class NutritionEntryService : INutritionEntryService
{
    private readonly AppDbContext _db;

    public NutritionEntryService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<NutritionEntry>> GetAllAsync(int? trainingWeekId, string? title)
    {
        var query = _db.NutritionEntries.AsQueryable();

        if (trainingWeekId.HasValue)
        {
            query = query.Where(n => n.TrainingWeekId == trainingWeekId.Value);
        }

        if (!string.IsNullOrWhiteSpace(title))
        {
            query = query.Where(n => n.Title.Contains(title));
        }

        return await query
            .OrderByDescending(n => n.Time)
            .ToListAsync();
    }

    public async Task<NutritionEntry?> GetByIdAsync(int id)
    {
        return await _db.NutritionEntries.FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task<NutritionEntry?> CreateAsync(NutritionEntry entry)
    {
        var weekExists = await _db.TrainingWeeks.AnyAsync(w => w.Id == entry.TrainingWeekId);
        if (!weekExists)
        {
            return null;
        }

        _db.NutritionEntries.Add(entry);
        await _db.SaveChangesAsync();
        return entry;
    }

    public async Task<NutritionEntryUpdateResult> UpdateAsync(int id, NutritionEntry updated)
    {
        var existing = await _db.NutritionEntries.FirstOrDefaultAsync(n => n.Id == id);
        if (existing is null)
        {
            return NutritionEntryUpdateResult.NotFound;
        }

        var weekExists = await _db.TrainingWeeks.AnyAsync(w => w.Id == updated.TrainingWeekId);
        if (!weekExists)
        {
            return NutritionEntryUpdateResult.InvalidTrainingWeek;
        }

        existing.Title = updated.Title;
        existing.Time = updated.Time;
        existing.Calories = updated.Calories;
        existing.ProteinGrams = updated.ProteinGrams;
        existing.CarbsGrams = updated.CarbsGrams;
        existing.FatGrams = updated.FatGrams;
        existing.TrainingWeekId = updated.TrainingWeekId;

        await _db.SaveChangesAsync();
        return NutritionEntryUpdateResult.Updated;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.NutritionEntries.FirstOrDefaultAsync(n => n.Id == id);
        if (existing is null)
        {
            return false;
        }

        _db.NutritionEntries.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}