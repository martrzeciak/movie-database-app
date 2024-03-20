namespace MovieDatabaseAPI.DTOs
{
    public class ActorDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string Birthplace { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public int HeightInCentimeters { get; set; }
        public string Gender { get; set; } = string.Empty;
        public double AverageRating { get; set; }
        public int RatingCount { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}

