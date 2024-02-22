namespace MovieDatabaseAPI.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string KnownAs { get; set; }
        public string Gender { get; set; }
        public string? Introduction { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string? Localization { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
