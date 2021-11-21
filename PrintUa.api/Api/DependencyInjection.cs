using Business;
using Business.Contract.Services.Entity;
using Business.Interface;
using Business.Interface.Services;
using Business.Service.Entity;
using Business.Services;
using Data.Interface.UnitOfWork;
using Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using UserIdentity.Data;

namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            //add here new services
            services.AddTransient<IOrderUnitOfWork, OrderUnitOfWork>();
            services.AddTransient<IProductServiceService, ProductServiceService>();
            services.AddTransient<IOrderUnitOfWork, OrderUnitOfWork>();
            services.AddTransient<IOrdersInfoService, OrdersInfoService>();
            services.AddTransient<IProfileDataService, ProfileDataService>();
            services.AddTransient<IProfileManager, ProfileManager<SystemUser>>();

            return services;
        }
    }
}
