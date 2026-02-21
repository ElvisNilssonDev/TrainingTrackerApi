using TrainingTrackerApi.Models;

namespace TrainingTrackerApi.Services;

public interface ITrainingDayService
{
    Task<List<TrainingDay>> GetAllAsync(int? trainingWeekId, string? search);
    Task<TrainingDay?> GetByIdAsync(int id);
    Task<TrainingDay?> CreateAsync(TrainingDay day);
    Task<TrainingDayUpdateResult> UpdateAsync(int id, TrainingDay updated);
    Task<bool> DeleteAsync(int id);
}

public enum TrainingDayUpdateResult
{
    Updated,
    NotFound,
    InvalidTrainingWeek
}
