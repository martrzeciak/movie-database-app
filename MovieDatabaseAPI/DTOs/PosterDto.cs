namespace MovieDatabaseAPI.DTOs
{
    public class PosterDto
    {
        public Guid Id { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public bool IsMain { get; set; }
    }
}
