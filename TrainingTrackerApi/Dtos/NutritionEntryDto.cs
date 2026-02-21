namespace TrainingTrackerApi.Dtos
{
    public class NutritionEntryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Time { get; set; }
        public int Calories { get; set; }
        public int? ProteinGrams { get; set; }
        public int? CarbsGrams { get; set; }        
        public int? FatGrams { get; set; }
        public int TrainingDayId { get; set; }
    }
}
