using Microsoft.AspNetCore.Identity;

namespace MovieDatabaseAPI.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string KnownAs { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Introduction { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string Localization { get; set; } = string.Empty;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        //public Guid? ImageId { get; set; }
        //public Image? Image { get; set; }
    }
}
