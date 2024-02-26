using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Data;
using MovieDatabaseAPI.Helpers;
using MovieDatabaseAPI.Interfaces;
using MovieDatabaseAPI.Services;

namespace MovieDatabaseAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddCors();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
