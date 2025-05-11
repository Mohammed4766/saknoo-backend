using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Saknoo.Application.Ads.Commands.UpdateAdCommand;
using Saknoo.Application.User;


namespace Saknoo.Application.Extensions;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ApplicationServiceRegistration).Assembly;

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(applicationAssembly);

        foreach (var type in applicationAssembly.GetTypes())
        {
            if (type.IsClass && !type.IsAbstract && typeof(IValidator).IsAssignableFrom(type))
            {

                if (type.Name != nameof(UpdateAdDtoValidator))
                {
                    var interfaceType = type.GetInterfaces()
                        .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>));

                    if (interfaceType != null)
                    {
                        services.AddScoped(interfaceType, type);
                    }
                }
            }
        }

        services.AddFluentValidationAutoValidation();

        services.AddScoped<IUserContext, UserContext>();
        return services;
    }
}
