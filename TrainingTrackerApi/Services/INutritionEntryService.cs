using TrainingTrackerApi.Models;

namespace TrainingTrackerApi.Services;

public interface INutritionEntryService
{
    Task<List<NutritionEntry>> GetAllAsync(int? trainingWeekId, string? title);
    Task<NutritionEntry?> GetByIdAsync(int id);
    Task<NutritionEntry?> CreateAsync(NutritionEntry entry);
    Task<NutritionEntryUpdateResult> UpdateAsync(int id, NutritionEntry updated);
    Task<bool> DeleteAsync(int id);
}

public enum NutritionEntryUpdateResult
{
    Updated,
    NotFound,
    InvalidTrainingWeek
}


