using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<User> userManager)
        {
            if (await userManager.Users.AnyAsync()) return;

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
            }
        }
    }
}
