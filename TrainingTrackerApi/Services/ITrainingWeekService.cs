using TrainingTrackerApi.Models;

namespace TrainingTrackerApi.Services;

public interface ITrainingWeekService
{
    Task<List<TrainingWeek>> GetAllAsync(string? search);
    Task<TrainingWeek?> GetByIdAsync(int id);
    Task<TrainingWeek> CreateAsync(TrainingWeek week);
    Task<bool> UpdateAsync(int id, TrainingWeek updated);
    Task<bool> DeleteAsync(int id);
}
