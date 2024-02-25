using Microsoft.AspNetCore.Identity;

namespace MovieDatabaseAPI.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
