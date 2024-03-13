using Microsoft.EntityFrameworkCore;

namespace MovieDatabaseAPI.Entities
{
    public class MovieRating
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public int Rating { get; set; }
    }
}
