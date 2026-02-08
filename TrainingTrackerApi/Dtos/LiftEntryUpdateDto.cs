using System.ComponentModel.DataAnnotations;

namespace TrainingTrackerApi.Dtos;

public class LiftEntryUpdateDto
{
    [Required, MinLength(3)]
    public string Title { get; set; } = null!;

    [Required]
    public DateTime Time { get; set; }

    [Required, MinLength(2)]
    public string Exercise { get; set; } = null!;

    [Range(0, 2000)]
    public decimal WeightKg { get; set; }

    [Range(1, 200)]
    public int Reps { get; set; }

    [Range(1, 50)]
    public int Sets { get; set; }

    [Required]
    public int TrainingWeekId { get; set; }
}

