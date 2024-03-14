using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Data
{
    public class DataContext : IdentityDbContext<User, AppRole, Guid,
        IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Poster> Posters { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<ActorImage> ActorImages { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }
        public DbSet<ActorRating> ActorRatings { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                .HasOne(u => u.User)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<UserRole>()
                .HasOne(u => u.Role)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            modelBuilder.Entity<MovieRating>()
                .HasKey(mr => new { mr.UserId, mr.MovieId });

            modelBuilder.Entity<MovieRating>()
                .HasOne(u => u.User)
                .WithMany(mr => mr.MovieRatings)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<MovieRating>()
                .HasOne(u => u.Movie)
                .WithMany(ur => ur.MovieRatings)
                .HasForeignKey(ur => ur.MovieId)
                .IsRequired();

            modelBuilder.Entity<ActorRating>()
                .HasKey(ar => new { ar.UserId, ar.ActorId });

            modelBuilder.Entity<ActorRating>()
                .HasOne(u => u.User)
                .WithMany(ar => ar.ActorRatings)
                .HasForeignKey(ar => ar.UserId)
                .IsRequired();

            modelBuilder.Entity<ActorRating>()
                .HasOne(u => u.Actor)
                .WithMany(ar => ar.ActorRatings)
                .HasForeignKey(ar => ar.ActorId)
                .IsRequired();
        }
    }
}
