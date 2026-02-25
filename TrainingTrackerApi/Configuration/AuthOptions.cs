namespace TrainingTrackerApi.Configuration
{
    public class AuthOptions
    {
        public const string SectionName = "Auth";
        public List<DemoUser> user { get; init; } = [];
    }

    public class DemoUser
    {
        public required string Username { get; init; }
        public required string Password { get; init; }
        public required string Role { get; init; }
    }
}
