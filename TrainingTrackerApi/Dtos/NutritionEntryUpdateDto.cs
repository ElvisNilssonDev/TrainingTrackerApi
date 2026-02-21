using System.ComponentModel.DataAnnotations;

namespace TrainingTrackerApi.Dtos
{
    public class NutritionEntryUpdateDto
    {
        [Required, MinLength(3)]
        public string Title { get; set; } = "Daily calories";

        [Required]
        public DateTime Time { get; set; }

        [Range(0, 20000)]
        public int Calories { get; set; }

        [Range(0, 500)]
        public int? ProteinGrams { get; set; }

        [Range(0, 2000)]
        public int? CarbsGrams { get; set; }

        [Range(0, 1000)]
        public int? FatGrams { get; set; }

        [Required]
        public int TrainingWeekId { get; set; }

    }
}
