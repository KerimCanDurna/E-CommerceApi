using Core.Repositories;
using Domain.IServices;

using Domain.IServices.IRepositories;
using Domain.IServices.ISharedIdentity;
using Domain.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class CoreServiceRegistration
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
             //services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("ECommerce"));

          

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<IParentCategoryRepository, ParentCategoryRepository>();
            services.AddScoped(typeof(IUserRepository<>), typeof(UserRepository<>));
            services.AddScoped(typeof (IBasketRepository<>),typeof(BasketRepository<>));
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();







            return services;
        }
    }
}
