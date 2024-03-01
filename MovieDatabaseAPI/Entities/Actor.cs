﻿namespace MovieDatabaseAPI.Entities
{
    public class Actor
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Birthplace { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public int HeightInCentimeters { get; set; }

        public ActorImage ActorImage { get; set; } = null!;
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}