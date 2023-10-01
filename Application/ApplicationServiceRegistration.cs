using Application.Features.Baskets.Dto;
using Application.Features.TokenIdentity.TokenService;
using Microsoft.Extensions.DependencyInjection;
using Shared.PipelineValidation;
using System.Reflection;
using FluentValidation;


namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<UserService>();
            services.AddScoped<CustomAuthenticationService>();
            services.AddScoped<TokenServices>();
            services.AddScoped<BasketDto>();






            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
               
            });

            return services;
        }

        public static IServiceCollection AddSubClassesOfType(
           this IServiceCollection services,
           Assembly assembly,
           Type type,
           Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
       )
        {
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
            foreach (var item in types)
                if (addWithLifeCycle == null)
                    services.AddScoped(item);

                else
                    addWithLifeCycle(services, type);
            return services;
        }
    }
}
