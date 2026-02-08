namespace TrainingTrackerApi.Models;

public class TrainingWeek
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = "";

    public DateTime WeekStart { get; set; }

    public ICollection<LiftEntry> LiftEntries { get; set; } = new List<LiftEntry>();
    public ICollection<NutritionEntry> NutritionEntries { get; set; } = new List<NutritionEntry>();
}
