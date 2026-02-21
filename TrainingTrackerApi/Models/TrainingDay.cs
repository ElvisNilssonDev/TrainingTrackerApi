namespace TrainingTrackerApi.Models;

public class TrainingDay
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = "";

    public DateTime Date { get; set; }

    public int TrainingWeekId { get; set; }
    public TrainingWeek TrainingWeek { get; set; } = null!;

    public ICollection<LiftEntry> LiftEntries { get; set; } = new List<LiftEntry>();
    public ICollection<NutritionEntry> NutritionEntries { get; set; } = new List<NutritionEntry>();
}
