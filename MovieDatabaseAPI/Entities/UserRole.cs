using Microsoft.AspNetCore.Identity;

namespace MovieDatabaseAPI.Entities
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public User User { get; set; } = null!;
        public AppRole Role { get; set; } = null!;
    }
}
