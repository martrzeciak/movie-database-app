using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseAPI.DTOs
{
    public class LoginDto
    {
        public string UserNameOrEmail { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
}
