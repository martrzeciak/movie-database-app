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
                    //Image = new Image() { ImageUrl = "https://randomuser.me/api/portraits/men/75.jpg" }
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "Jane31",
                    KnownAs = "Jane Doe",
                    Gender = "Female",
                    Introduction = "Hi, I'm Jane Doe!",
                    Localization = "en-US",
                    //Image = new Image() { ImageUrl = "https://randomuser.me/api/portraits/women/76.jpg" }
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

            // add actors
            List<Actor> actors = new List<Actor>
            {
                new Actor // 0
                {
                    FirstName = "Morgan",
                    LastName = "Freeman",
                    DateOfBirth = new DateTime(1947, 6, 1),
                    Birthplace = "Memphis, Tennessee, USA",
                    Biography =
                        "Born in Memphis, Tennessee, Freeman was raised " +
                        "in Mississippi, where he began acting in school " +
                        "plays. He studied theater arts in Los Angeles " +
                        "and appeared in stage productions in his early " +
                        "career. He rose to fame in the 1970s for his " +
                        "role in the children's television series The " +
                        "Electric Company.",
                    HeightInCentimeters = 188,
                    ActorImage = new ActorImage() { ImageUrl = "Image Placeholder" }
                },
                new Actor // 1
                {
                    FirstName = "Tim",
                    LastName = "Robbins",
                    DateOfBirth = new DateTime(1958, 10, 16),
                    Birthplace = "West Covina, Kalifornia, USA",
                    Biography =
                        "Timothy Francis Robbins is an American actor, " +
                        "screenwriter, director, producer, activist, and " +
                        "amateur musician. He has won many awards in his " +
                        "career, including the Academy Award in 2004 for " +
                        "his work in Mystic River. He was born in West Covina, " +
                        "Los Angeles County and raised in New York City.",
                    HeightInCentimeters = 196,
                    ActorImage = new ActorImage() { ImageUrl = "Image Placeholder" }
                },
                new Actor // 2
                {
                    FirstName = "James",
                    LastName = "Whitmore",
                    DateOfBirth = new DateTime(1921, 10, 1),
                    Birthplace = "White Plains, New York, USA",
                    Biography =
                        "James Allen Whitmore Jr. was an American film, " +
                        "theatre, and television actor. He won a Tony Award, " +
                        "an Emmy Award, and was nominated for two Academy Awards. " +
                        "Whitmore is well known for his performances in " +
                        "films such as The Shawshank Redemption, " +
                        "The Asphalt Jungle, and Give 'em Hell, Harry!",
                    HeightInCentimeters = 183,
                    ActorImage = new ActorImage() { ImageUrl = "Image Placeholder" }
                },
                new Actor // 3
                {
                    FirstName = "Bob",
                    LastName = "Gunton",
                    DateOfBirth = new DateTime(1945, 11, 15),
                    Birthplace = "Santa Monica, California, USA",
                    Biography =
                        "Bob Gunton is an American actor, best known for " +
                        "his role as Warden Norton in the critically acclaimed " +
                        "film The Shawshank Redemption. With a career spanning " +
                        "across film, television, and theater, Gunton has " +
                        "established himself as a versatile actor.",
                    HeightInCentimeters = 183,
                    ActorImage = new ActorImage() { ImageUrl = "Image Placeholder" }
                },

                new Actor // 4
                {
                    FirstName = "Clancy",
                    LastName = "Brown",
                    DateOfBirth = new DateTime(1959, 1, 5),
                    Birthplace = "Urbana, Ohio, USA",
                    Biography =
                        "Clancy Brown is an American actor and voice actor, " +
                        "known for his deep and resonant voice. He has " +
                        "appeared in various films and television series, " +
                        "with notable roles in The Shawshank Redemption, " +
                        "Highlander, and the voice of Mr. Krabs in SpongeBob " +
                        "SquarePants. Brown has showcased his talent across " +
                        "a wide range of genres.",
                    HeightInCentimeters = 196,
                    ActorImage = new ActorImage() { ImageUrl = "Image Placeholder" }
                },
                // the goodfather
                new Actor // 5
                {
                    FirstName = "Marlon",
                    LastName = "Brando",
                    DateOfBirth = new DateTime(1924, 4, 3),
                    Birthplace = "Omaha, Nebraska, USA",
                    Biography =
                        "Marlon Brando was an American actor and film director. " +
                        "Regarded as one of the greatest actors in film history, " +
                        "he is best known for his performances in A Streetcar Named " +
                        "Desire, On the Waterfront, and, of course, The Godfather.",
                    HeightInCentimeters = 175,
                    ActorImage = new ActorImage() { ImageUrl = "Image Placeholder" }
                },
                new Actor // 6
                {
                    FirstName = "Al",
                    LastName = "Pacino",
                    DateOfBirth = new DateTime(1940, 4, 25),
                    Birthplace = "New York City, New York, USA",
                    Biography =
                        "Al Pacino is an American actor and filmmaker. He has " +
                        "received numerous awards, including an Academy Award, " +
                        "a Tony Award, and a Primetime Emmy Award. Pacino is known " +
                        "for his roles in The Godfather series, Scarface, and " +
                        "Scent of a Woman.",
                    HeightInCentimeters = 170,
                    ActorImage = new ActorImage() { ImageUrl = "Image Placeholder" }
                },
                new Actor // 7
                {
                    FirstName = "James",
                    LastName = "Caan",
                    DateOfBirth = new DateTime(1940, 3, 26),
                    Birthplace = "The Bronx, New York City, New York, USA",
                    Biography =
                        "James Caan is an American actor. He has appeared in more " +
                        "than 60 films, including The Godfather, Misery, and Elf. " +
                        "Caan received an Academy Award nomination for his role " +
                        "in The Godfather.",
                    HeightInCentimeters = 175,
                    //Image = new Image() { ImageUrl = "Image Placeholder" }
                },
                new Actor // 8
                {
                    FirstName = "Robert",
                    LastName = "Duvall",
                    DateOfBirth = new DateTime(1931, 1, 5),
                    Birthplace = "San Diego, California, USA",
                    Biography =
                        "Robert Duvall is an American actor and filmmaker. He has " +
                        "received multiple awards, including an Academy Award, " +
                        "four Golden Globe Awards, and a BAFTA Award. Duvall is " +
                        "known for his roles in The Godfather, Apocalypse Now, " +
                        "and Tender Mercies.",
                    HeightInCentimeters = 174,
                    //Image = new Image() { ImageUrl = "Image Placeholder" }
                },
                new Actor // 9
                {
                    FirstName = "Diane",
                    LastName = "Keaton",
                    DateOfBirth = new DateTime(1946, 1, 5),
                    Birthplace = "Los Angeles, California, USA",
                    Biography =
                        "Diane Keaton is an American actress and filmmaker. She is " +
                        "known for her roles in Annie Hall, The Godfather series, " +
                        "and Something's Gotta Give. Keaton has received various " +
                        "awards, including an Academy Award and a BAFTA Award.",
                    HeightInCentimeters = 170,
                    //Image = new Image() { ImageUrl = "Image Placeholder" }
                },
            };
            await dataContext.Actors.AddRangeAsync(actors);
            await dataContext.SaveChangesAsync();

            // add movies
            List<Movie> movies = new List<Movie>
            {
                new Movie // 0
                {
                    Title = "The Shawshank Redemption",
                    ReleaseDate = 1994,
                    DurationInMinutes = 144,
                    Genres = new List<Genre> { genres[1] },
                    Director = "Frank Darabont",
                    Description =
                        "Over the course of several years, " +
                        "two convicts form a friendship, seeking " +
                        "consolation and, eventually, redemption " +
                        "through basic compassion.",
                    Poster = new Poster() { PosterUrl = "Image Placeholder" },
                    Actors = new List<Actor> { actors[0], actors[1] }

                },
                new Movie // 1
                {
                    Title = "The Godfather",
                    ReleaseDate = 1972,
                    DurationInMinutes = 175,
                    Genres = new List<Genre> { genres[1], genres[6] },
                    Director = "Francis Ford Coppola",
                    Description =
                        "The aging patriarch of an organized crime " +
                        "dynasty transfers control of his clandestine " +
                        "empire to his reluctant son.",
                    Poster = new Poster() { PosterUrl = "Image Placeholder" },
                    Actors = new List<Actor> { actors[2], actors[3] }
                },
                new Movie // 2
                {
                    Title = "The Dark Knight",
                    ReleaseDate = 2008,
                    DurationInMinutes = 152,
                    Genres = new List<Genre> { genres[0], genres[6], genres[1] },
                    Director = "Christopher Nolan",
                    Description =
                        "When the menace known as the Joker wreaks havoc and " +
                        "chaos on the people of Gotham, Batman must accept one " +
                        "of the greatest psychological and physical tests of " +
                        "his ability to fight injustice.",
                    Poster = new Poster() { PosterUrl = "Image Placeholder" },
                    Actors = new List<Actor> { actors[5], actors[6] }
                },
                //new Movie // 3
                //{
                //    Title = "12 Angry Men",
                //    ReleaseDate = 1957,
                //    DurationInMinutes = 152,
                //    Genres = new List<Genre> { genres[6], genres[1] },
                //    Director = "Sidney Lumet",
                //    Description =
                //        "The jury in a New York City murder trial is frustrated " +
                //        "by a single member whose skeptical caution forces them " +
                //        "to more carefully consider the evidence before jumping to " +
                //        "a hasty verdict.",
                //    Image = new Image() { ImageUrl = "Image Placeholder" }
                //},
                //new Movie // 4
                //{
                //    Title = "Lista Schindlera",
                //    ReleaseDate = 1993,
                //    DurationInMinutes = 195,
                //    Genres = new List<Genre> { genres[5], genres[1], genres[11] },
                //    Director = "Steven Spielberg",
                //    Description =
                //        "In German-occupied Poland during World War II, industrialist " +
                //        "Oskar Schindler gradually becomes concerned for his Jewish " +
                //        "workforce after witnessing their persecution by the Nazis.",
                //    Image = new Image() { ImageUrl = "Image Placeholder" }
                //},
                //new Movie // 5
                //{
                //    Title = "Terminator 2: Judgment Day",
                //    ReleaseDate = 1991,
                //    DurationInMinutes = 137,
                //    Genres = new List<Genre> { genres[0], genres[17] },
                //    Director = "James Cameron",
                //    Description =
                //        "A cyborg, identical to the one who failed to kill Sarah Connor, must " +
                //        "now protect her ten year old son John from an even more advanced and " +
                //        "powerful cyborg.",
                //    Image = new Image() { ImageUrl = "Image Placeholder" }
                //},
                //new Movie // 6
                //{
                //    Title = "The Lord of the Rings: The Return of the King",
                //    ReleaseDate = 2003,
                //    DurationInMinutes = 201,
                //    Genres = new List<Genre> { genres[0], genres[1], genres[3] },
                //    Director = "Peter Jackson",
                //    Description =
                //        "Gandalf and Aragorn lead the World of Men against Sauron's " +
                //        "army to draw his gaze from Frodo and Sam as they approach " +
                //        "Mount Doom with the One Ring.",
                //    Image = new Image() { ImageUrl = "Image Placeholder" }
                //},
                //new Movie // 7
                //{
                //    Title = "Pulp Fiction",
                //    ReleaseDate = 1994,
                //    DurationInMinutes = 153,
                //    Genres = new List<Genre> { genres[1], genres[6] },
                //    Director = "Quentin Tarantino",
                //    Description =
                //        "The lives of two mob hitmen, a boxer, a gangster and his wife, " +
                //        "and a pair of diner bandits intertwine in four tales of violence " +
                //        "and redemption.",
                //    Image = new Image() { ImageUrl = "Image Placeholder" }
                //},
                //new Movie // 8
                //{
                //    Title = "Fight Club",
                //    ReleaseDate = 1999,
                //    DurationInMinutes = 139,
                //    Genres = new List<Genre> { genres[1] },
                //    Director = "David Fincher",
                //    Description =
                //        "An insomniac office worker and a devil-may-care soap maker form an " +
                //        "underground fight club that evolves into much more.",
                //    Image = new Image() { ImageUrl = "Image Placeholder" }
                //},
                //new Movie // 9
                //{
                //    Title = "The Matrix",
                //    ReleaseDate = 1999,
                //    DurationInMinutes = 136,
                //    Genres = new List<Genre> { genres[0], genres[17] },
                //    Director = "Lana Wachowski, Lilly Wachowski",
                //    Description =
                //        "When a beautiful stranger leads computer hacker Neo to a forbidding " +
                //        "underworld, he discovers the shocking truth--the life he knows is " +
                //        "the elaborate deception of an evil cyber-intelligence.",
                //    Image = new Image() { ImageUrl = "Image Placeholder" }
                //},
            };

            await dataContext.Movies.AddRangeAsync(movies);
            await dataContext.SaveChangesAsync();
        }
    }
}
