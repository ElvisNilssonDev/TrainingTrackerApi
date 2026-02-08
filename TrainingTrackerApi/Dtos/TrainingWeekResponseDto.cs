namespace TrainingTrackerApi.Dtos;

public class TrainingWeekResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = "";
    public DateTime WeekStart { get; set; }
}
