namespace TrainingTrackerApi.Dtos;

public class LiftEntryResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime Time { get; set; }
    public string Exercise { get; set; } = null!;
    public decimal WeightKg { get; set; }
    public int Reps { get; set; }
    public int Sets { get; set; }
    public int TrainingDayId { get; set; }
}
