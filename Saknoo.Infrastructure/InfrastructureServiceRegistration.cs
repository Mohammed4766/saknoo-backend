using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Saknoo.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Saknoo.Domain.Entities;
using Saknoo.Infrastructure.Services;
using Saknoo.Domain.Interfaces;
using Saknoo.Infrastructure.Data.Seed;


namespace Saknoo.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = false;
            options.User.AllowedUserNameCharacters = "0123456789";
            options.SignIn.RequireConfirmedAccount = false;

        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<IApplicationDbContextSeed, ApplicationDbContextSeed>();
        services.AddScoped<ITokenService, TokenService>();


        return services;
    }
}
