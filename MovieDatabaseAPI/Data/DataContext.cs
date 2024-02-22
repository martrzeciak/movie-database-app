using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Entities;

namespace MovieDatabaseAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {

        }


    }
}
