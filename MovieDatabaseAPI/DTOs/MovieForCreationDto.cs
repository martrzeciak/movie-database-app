namespace MovieDatabaseAPI.DTOs
{
    public class MovieForCreationDto
    {
        public string Title { get; set; } = string.Empty;
        public int ReleaseDate { get; set; }
        public int DurationInMinutes { get; set; }
        public string Director { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<Guid> GenreIds { get; set; } = new List<Guid>();
        public List<Guid> ActorIds { get; set; } = new List<Guid>();
    }
}
