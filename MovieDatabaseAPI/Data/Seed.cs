using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var usersToSeed = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    KnownAs = "John Doe",
                    Gender = "Male",
                    Introduction = "Hello, I'm John Doe!",
                    Localization = "en-US",
                    PhotoUrl = "https://randomuser.me/api/portraits/men/75.jpg"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    KnownAs = "Jane Doe",
                    Gender = "Female",
                    Introduction = "Hi, I'm Jane Doe!",
                    Localization = "en-US",
                    PhotoUrl = "https://randomuser.me/api/portraits/women/76.jpg"
                },
            };

            context.Users.AddRange(usersToSeed);

            await context.SaveChangesAsync();
        }
    }
}
