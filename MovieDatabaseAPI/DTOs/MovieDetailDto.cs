using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.DTOs
{
    public class MovieDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public int ReleaseDate { get; set; }
        public int DurationInMinutes { get; set; }
        public string Director { get; set; } = String.Empty;
        public string PosterUrl { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;

        public ICollection<GenreDto> Genres { get; set; } = null!;
    }
}
