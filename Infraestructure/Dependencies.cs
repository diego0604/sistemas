using Infrastructure.Data.Abstractions;
using Infrastructure.Data.Infrastructure.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.EntityFrameworkCore.Extensions;

namespace Infrastructure
{
    public static class Dependencies
    {
        public static IServiceCollection AddDbContextImplementation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IAuthDbContext, AuthDbContext>(options =>
                   options.UseMySQL(
                   configuration.GetConnectionString("AuthDbConnection")!
               ));

            services.AddDbContext<IAppDbContext, AppDbContext>(options =>
                   options.UseMySQL(
                   configuration.GetConnectionString("FarmaciaDbConnection")!
               ));


            return services;
        }

    }
}
