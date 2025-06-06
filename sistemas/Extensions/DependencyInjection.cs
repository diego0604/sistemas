using Application.Services.Abstractions;
using Application.Services.Implementations;
using Domain.Repositories.Abstractions;
using Infrastructure;
using Infrastructure.Repositories.Implementations;
namespace sistemas.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextImplementation(configuration);

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISalesService, SalesService>();
            services.AddScoped<IDashboardService, DashboardService>();

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        public static WebApplication MapSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }

    }
}
