using System.ComponentModel.DataAnnotations;

namespace TrainingTrackerApi.Dtos;

public class TrainingWeekCreateDto
{
    [Required]
    [MinLength(3)]
    public string Title { get; set; } = null!;

    public string Description { get; set; } = "";

    // Måndag som start rekommenderas, men vi nöjer oss med “inte i dåtid”
    [Required]
    public DateTime WeekStart { get; set; }
}
