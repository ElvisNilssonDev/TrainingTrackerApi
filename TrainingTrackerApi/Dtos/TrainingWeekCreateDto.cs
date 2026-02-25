using System.ComponentModel.DataAnnotations;

namespace TrainingTrackerApi.Dtos;

public class TrainingWeekCreateDto
{
    [Required]
    [MinLength(3)]
    public string Title { get; set; } = null!;

    public string Description { get; set; } = "";

    
    [Required]
    public DateTime WeekStart { get; set; }
}
