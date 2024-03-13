using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.Data;
using MovieDatabaseAPI.Data.Repositories;
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
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();

            return services;
        }
    }
}
