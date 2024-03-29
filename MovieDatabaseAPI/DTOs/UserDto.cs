﻿namespace MovieDatabaseAPI.DTOs
{
    public class UserDto
    {
        public string UserName { get; set; } = String.Empty;
        public string Token { get; set; } = String.Empty;
        public string? ImageUrl { get; set; }
        public string Gender { get; set; } = String.Empty;
    }
}
