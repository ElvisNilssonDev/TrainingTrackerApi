namespace TrainingTrackerApi.Models
{
    public class NutritionEntry
    {
        public int Id { get; set; }
        public string Title { get; set; } = "Daily calories";
        public DateTime Time { get; set; }

        public int Calories { get; set; }
        public int? ProteinGrams { get; set; }
        public int? CarbsGrams { get; set; }
        public int? FatGrams { get; set; }

        public int TrainingWeekId { get; set; }
        public TrainingWeek TrainingWeek { get; set; } = null!;
    }
}
