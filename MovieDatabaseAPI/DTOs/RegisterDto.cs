﻿using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; } = String.Empty;
        [Required]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string Gender { get; set; } = String.Empty;
        [Required]
        [StringLength(25, MinimumLength = 4)]
        public string Password { get; set; } = String.Empty;
    }
}
