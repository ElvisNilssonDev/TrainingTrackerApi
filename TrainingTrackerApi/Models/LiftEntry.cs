namespace TrainingTrackerApi.Models
{
    public class LiftEntry
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!; // ex: "Bench Press - top set"
        public DateTime Time { get; set; }

        public string Exercise { get; set; } = null!;
        public decimal WeightKg { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }

        public int TrainingDayId { get; set; }
        public TrainingDay TrainingDay { get; set; } = null!;
    }
}
