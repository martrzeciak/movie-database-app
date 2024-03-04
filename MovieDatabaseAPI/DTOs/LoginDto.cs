using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseAPI.DTOs
{
    public class LoginDto
    {
        public string UserName { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
}
