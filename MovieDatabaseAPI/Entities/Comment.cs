﻿namespace MovieDatabaseAPI.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string CommentContent { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsEdited { get; set; } = false;

        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<User> Likes { get; set; } = new List<User>();
    }
}
