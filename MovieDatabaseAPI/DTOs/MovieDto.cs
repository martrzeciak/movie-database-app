﻿namespace MovieDatabaseAPI.DTOs
{
    public class MovieDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public int ReleaseDate { get; set; }
        public int DurationInMinutes { get; set; }
        public string Director { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public ICollection<GenreDto> Genres { get; set; } = null!;
    }
}