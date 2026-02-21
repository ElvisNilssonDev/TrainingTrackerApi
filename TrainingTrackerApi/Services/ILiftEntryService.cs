using TrainingTrackerApi.Models;

namespace TrainingTrackerApi.Services;

public interface ILiftEntryService
{
    Task<List<LiftEntry>> GetAllAsync(int? trainingWeekId, string? exercise);
    Task<LiftEntry?> GetByIdAsync(int id);
    Task<LiftEntry?> CreateAsync(LiftEntry entry);
    Task<LiftEntryUpdateResult> UpdateAsync(int id, LiftEntry updated);
    Task<bool> DeleteAsync(int id);
}

public enum LiftEntryUpdateResult
{
    Updated,
    NotFound,
    InvalidTrainingWeek
}
