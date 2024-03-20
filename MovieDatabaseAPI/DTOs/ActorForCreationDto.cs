namespace MovieDatabaseAPI.DTOs
{
    public class ActorForCreationDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string Birthplace { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public int HeightInCentimeters { get; set; }
        public string Gender { get; set; } = string.Empty;
        public List<Guid> MovieIds { get; set; } = new List<Guid>();
    }
}
