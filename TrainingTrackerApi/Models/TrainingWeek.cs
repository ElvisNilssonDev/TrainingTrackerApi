namespace TrainingTrackerApi.Models;

public class TrainingWeek
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = "";

    public DateTime WeekStart { get; set; }

    public ICollection<TrainingDay> TrainingDays { get; set; } = new List<TrainingDay>();
}
