using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext dataContext, UserManager<User> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            // add roles
            var roles = new List<AppRole>
            {
                new AppRole{Name = "User" },
                new AppRole{Name = "Admin" }
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            // add users
            var usersToSeed = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "John123",
                    KnownAs = "John Doe",
                    Gender = "Male",
                    Introduction = "Hello, I'm John Doe!",
                    Localization = "en-US",
                    PhotoUrl = "https://randomuser.me/api/portraits/men/75.jpg"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "Jane31",
                    KnownAs = "Jane Doe",
                    Gender = "Female",
                    Introduction = "Hi, I'm Jane Doe!",
                    Localization = "en-US",
                    PhotoUrl = "https://randomuser.me/api/portraits/women/76.jpg"
                },
            };

            foreach (var user in usersToSeed)
            {
                user.UserName = user.UserName!.ToLower();
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "User");
            }

            var admin = new User
            {
                UserName = "admin",
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRoleAsync(admin, "Admin");

            // add genres
            List<Genre> genres = new List<Genre>
            {
                new Genre { Name = "Action" }, // 0
                new Genre { Name = "Drama" }, // 1
                new Genre { Name = "Comedy" }, // 2
                new Genre { Name = "Adventure" }, // 3
                new Genre { Name = "Animation" }, // 4
                new Genre { Name = "Biography" }, // 5
                new Genre { Name = "Crime" }, // 6
                new Genre { Name = "Documentary" }, // 7
                new Genre { Name = "Family" }, // 8
                new Genre { Name = "Fantasy" }, //9
                new Genre { Name = "Film Noir" }, // 10
                new Genre { Name = "History" }, // 11
                new Genre { Name = "Horror" }, // 12
                new Genre { Name = "Music" }, // 13
                new Genre { Name = "Musical" }, // 14
                new Genre { Name = "Mystery" }, // 15
                new Genre { Name = "Romance" }, // 16
                new Genre { Name = "Sci-Fi" }, // 17
                new Genre { Name = "Sport" }, // 18
                new Genre { Name = "Thriller" }, // 19
                new Genre { Name = "War" }, // 20
                new Genre { Name = "Western" }, // 21
                new Genre { Name = "Martial Arts" }, // 22
                new Genre { Name = "Period" }, // 23
                new Genre { Name = "Political" }, // 24
                new Genre { Name = "Psychological" }, // 25
                new Genre { Name = "Supernatural" }, // 26
                new Genre { Name = "Cyberpunk" }, // 27
            };

            await dataContext.Genres.AddRangeAsync(genres);
            await dataContext.SaveChangesAsync();


            List<Movie> movies = new List<Movie>
            {       
                new Movie // 0
                {
                    Title = "The Shawshank Redemption",
                    ReleaseDate = 1994,
                    DurationInMinutes = 144,
                    Genres = new List<Genre> { genres[1] },
                    Director = "Frank Darabont",
                    PosterUrl = "Placeholder Url",
                    Description = 
                        "Over the course of several years, " +
                        "two convicts form a friendship, seeking " +
                        "consolation and, eventually, redemption " +
                        "through basic compassion."
                },
                new Movie // 1
                {
                    Title = "The Godfather",
                    ReleaseDate = 1972,
                    DurationInMinutes = 175,
                    Genres = new List<Genre> { genres[1], genres[6] },
                    Director = "Francis Ford Coppola",
                    PosterUrl = "Placeholder Url",
                    Description = 
                        "The aging patriarch of an organized crime " +
                        "dynasty transfers control of his clandestine " +
                        "empire to his reluctant son."
                },
                new Movie // 2
                {
                    Title = "The Dark Knight",
                    ReleaseDate = 2008,
                    DurationInMinutes = 152,
                    Genres = new List<Genre> { genres[0], genres[6], genres[1] },
                    Director = "Christopher Nolan",
                    PosterUrl = "Placeholder Url",
                    Description =
                        "When the menace known as the Joker wreaks havoc and " +
                        "chaos on the people of Gotham, Batman must accept one " +
                        "of the greatest psychological and physical tests of " +
                        "his ability to fight injustice."
                },
                new Movie // 3
                {
                    Title = "12 Angry Men",
                    ReleaseDate = 1957,
                    DurationInMinutes = 152,
                    Genres = new List<Genre> { genres[6], genres[1] },
                    Director = "Sidney Lumet",
                    PosterUrl = "Placeholder Url",
                    Description =
                        "The jury in a New York City murder trial is frustrated " +
                        "by a single member whose skeptical caution forces them " +
                        "to more carefully consider the evidence before jumping to " +
                        "a hasty verdict."
                },
                new Movie // 4
                {
                    Title = "Lista Schindlera",
                    ReleaseDate = 1993,
                    DurationInMinutes = 195,
                    Genres = new List<Genre> { genres[5], genres[1], genres[11] },
                    Director = "Steven Spielberg",
                    PosterUrl = "Placeholder Url",
                    Description =
                        "In German-occupied Poland during World War II, industrialist " +
                        "Oskar Schindler gradually becomes concerned for his Jewish " +
                        "workforce after witnessing their persecution by the Nazis."
                },
                new Movie // 5
                {
                    Title = "Terminator 2: Judgment Day",
                    ReleaseDate = 1991,
                    DurationInMinutes = 137,
                    Genres = new List<Genre> { genres[0], genres[17] },
                    Director = "James Cameron",
                    PosterUrl = "Placeholder Url",
                    Description =
                        "A cyborg, identical to the one who failed to kill Sarah Connor, must " +
                        "now protect her ten year old son John from an even more advanced and " +
                        "powerful cyborg."
                },
                new Movie // 6
                {
                    Title = "The Lord of the Rings: The Return of the King",
                    ReleaseDate = 2003,
                    DurationInMinutes = 201,
                    Genres = new List<Genre> { genres[0], genres[1], genres[3] },
                    Director = "Peter Jackson",
                    PosterUrl = "Placeholder Url",
                    Description =
                        "Gandalf and Aragorn lead the World of Men against Sauron's " +
                        "army to draw his gaze from Frodo and Sam as they approach " +
                        "Mount Doom with the One Ring."
                },
                new Movie // 7
                {
                    Title = "Pulp Fiction",
                    ReleaseDate = 1994,
                    DurationInMinutes = 153,
                    Genres = new List<Genre> { genres[1], genres[6] },
                    Director = "Quentin Tarantino",
                    PosterUrl = "Placeholder Url",
                    Description =
                        "The lives of two mob hitmen, a boxer, a gangster and his wife, " +
                        "and a pair of diner bandits intertwine in four tales of violence " +
                        "and redemption."
                },
                new Movie // 8
                {
                    Title = "Fight Club",
                    ReleaseDate = 1999,
                    DurationInMinutes = 139,
                    Genres = new List<Genre> { genres[1] },
                    Director = "David Fincher",
                    PosterUrl = "Placeholder Url",
                    Description =
                        "An insomniac office worker and a devil-may-care soap maker form an " +
                        "underground fight club that evolves into much more."
                },
                new Movie // 9
                {
                    Title = "The Matrix",
                    ReleaseDate = 1999,
                    DurationInMinutes = 136,
                    Genres = new List<Genre> { genres[0], genres[17] },
                    Director = "Lana Wachowski, Lilly Wachowski",
                    PosterUrl = "Placeholder Url",
                    Description =
                        "When a beautiful stranger leads computer hacker Neo to a forbidding " +
                        "underworld, he discovers the shocking truth--the life he knows is " +
                        "the elaborate deception of an evil cyber-intelligence."
                },
            };

            await dataContext.Movies.AddRangeAsync(movies);
            await dataContext.SaveChangesAsync();
        }
    }
}
