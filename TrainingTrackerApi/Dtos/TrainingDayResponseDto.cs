namespace TrainingTrackerApi.Dtos;

public class TrainingDayResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = "";
    public DateTime Date { get; set; }
    public int TrainingWeekId { get; set; }
}
