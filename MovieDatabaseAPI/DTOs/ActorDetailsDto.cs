namespace MovieDatabaseAPI.DTOs
{
    public class ActorDetailsDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Birthplace { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public int HeightInCentimeters { get; set; }
        public string ActorImageUrl { get; set; } = string.Empty;
    }
}
