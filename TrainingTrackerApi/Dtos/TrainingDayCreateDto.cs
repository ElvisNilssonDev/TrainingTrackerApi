using System.ComponentModel.DataAnnotations;

namespace TrainingTrackerApi.Dtos;

public class TrainingDayCreateDto
{
    [Required, MinLength(3)]
    public string Title { get; set; } = null!;

    public string Description { get; set; } = "";

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int TrainingWeekId { get; set; }
}
