using Microsoft.AspNetCore.Identity;

namespace MovieDatabaseAPI.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Gender { get; set; } = string.Empty;
        public string Introduction { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string Localization { get; set; } = string.Empty;

        public ICollection<UserImage> UserImages { get; set; } = new List<UserImage>();
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<MovieRating> MovieRatings { get; set; } = new List<MovieRating>();
        public ICollection<ActorRating> ActorRatings { get; set; } = new List<ActorRating>();
    }
}
