using Microsoft.AspNetCore.Identity;

namespace MovieDatabaseAPI.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string KnownAs { get; set; } = String.Empty;
        public string Gender { get; set; } = String.Empty;
        public string Introduction { get; set; } = String.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string Localization { get; set; } = String.Empty;
        public string PhotoUrl { get; set; } = String.Empty;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
