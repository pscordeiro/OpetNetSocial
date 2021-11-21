using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OpetNet.Application.AutoMapper;
using OpetNet.Application.Interfaces;
using OpetNet.Application.Services;
using OpetNet.Domain.Interfaces;
using OpetNet.Infra.Data.Repository;

namespace OpetNet.Infra.CrossCutting.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services;
        }
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient(typeof(ICustomerAppService), typeof(CustomerAppService));

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));
            services.AddAutoMapper(typeof(CustomerViewModelToCustomer));
            services.AddAutoMapper(typeof(ViewModelToDomainMappingProfile));
            services.AddAutoMapper(typeof(EventToCommand));

            return services;
        }
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            return services;
        }
    }
}
