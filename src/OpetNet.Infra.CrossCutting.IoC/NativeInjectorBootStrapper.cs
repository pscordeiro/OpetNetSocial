using AutoMapper;
using log4net;
using log4net.Config;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpetNet.Application.AutoMapper;
using OpetNet.Domain.CommandHandlers;
using OpetNet.Domain.Commands;
using OpetNet.Domain.Core.Bus;
using OpetNet.Domain.Core.Notifications;
using OpetNet.Domain.EventHandlers;
using OpetNet.Domain.Events;
using OpetNet.Domain.Interfaces;
using OpetNet.Infra.CrossCutting.Bus;
using OpetNet.Infra.CrossCutting.Identity.Authorization;
using OpetNet.Infra.CrossCutting.Identity.Models;
using OpetNet.Infra.CrossCutting.Identity.Services;
using OpetNet.Infra.Data.UoW;
using SharedKernel.DomainEvents.CrossDomainEvents;
using SharedKernel.DomainEvents.CrossDomainEvents.Configuration;
using SharedKernel.DomainEvents.CrossDomainEvents.Interfaces;
using SharedKernel.Logger;
using SharedKernel.Logger.Interfaces;
using System;
using System.Data;
using System.IO;

namespace OpetNet.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));
            services.AddAutoMapper(typeof(ViewModelToDomainMappingProfile));
            services.AddAutoMapper(typeof(EventToCommand));

            //AutoMapperConfig.RegisterMappings();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Application
            //services.AddScoped<ICustomerAppService, CustomerAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerRemovedEvent>, CustomerEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewCustomerCommand, Unit>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, Unit>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand, Unit>, CustomerCommandHandler>();

            // Infra - Data
            //services.AddScoped<ICustomerRepository, CustomerRepository>();
            var connectionString = "configuration.GetConnectionString(DefaultConnection)";
            services.AddScoped<IDbConnection>(s => new SqlConnection(connectionString));
            services.AddScoped<IUnitOfWorkAdo, UnitOfWork>();
            services.AddScoped<IUnitOfWork>(s => s.GetService<IUnitOfWorkAdo>());


            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();

            // Crossdomain
            var crossdomainEventsSettings = configuration.GetSection("CrossDomainEventsSettings");
            services.AddSingleton(new CrossDomainEventsSettings
            {
                Host = crossdomainEventsSettings["Host"],
                User = crossdomainEventsSettings["User"],
                Password = crossdomainEventsSettings["Password"],
                VirtualHost = crossdomainEventsSettings["VirtualHost"],
                AutomaticRecoveryEnabled = Convert.ToBoolean(crossdomainEventsSettings["AutomaticRecoveryEnabled"]),
                ContinuationTimeout = TimeSpan.FromSeconds(Convert.ToInt32(crossdomainEventsSettings["ContinuationTimeout"])),
                NetworkRecoveryInterval = TimeSpan.FromSeconds(Convert.ToInt32(crossdomainEventsSettings["NetworkRecoveryInterval"])),
                RequestedHeartbeat = Convert.ToUInt16(crossdomainEventsSettings["RequestedHeartbeat"]),
                TopologyRecoveryEnabled = Convert.ToBoolean(crossdomainEventsSettings["TopologyRecoveryEnabled"])
            });

            services.AddSingleton<SharedKernel.DomainEvents.Core.IHandler<ICrossDomainEvent>, CrossDomainEventHandler>();
            services.AddSingleton<IChannelsManager, ChannelsManager>();
            services.AddScoped<IListener, Listener>();

            // log4net
            XmlConfigurator.ConfigureAndWatch(LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly()), new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));
            services.AddTransient<ILoggerRepository, LogRepository>();

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetService<ILoggerRepository>();
            services.AddSingleton(service.GetLogger("BNE"));
        }
    }
}
