using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Saknoo.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Saknoo.Domain.Entities;
using Saknoo.Infrastructure.Services;
using Saknoo.Domain.Interfaces;
using Saknoo.Infrastructure.Data.Seed;
using Saknoo.Domain.Repositories;
using Saknoo.Infrastructure.Repositories;
using Saknoo.Infrastructure.Configuration;
using Microsoft.Extensions.Options;


namespace Saknoo.Infrastructure.Extensions;

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
            options.Password.RequireNonAlphanumeric = false;

        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<IApplicationDbContextSeed, ApplicationDbContextSeed>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAdRepository, AdRepository>();
        services.AddScoped<ILookupRepository, LookupRepository>();
        services.AddScoped<IMatchingRepository, MatchingRepository>();
        services.AddScoped<IBlobStorageService, BlobStorageService>();




        services.Configure<BlobStorageSettings>(configuration.GetSection("AzureBlobStorage"));



        return services;
    }
}
