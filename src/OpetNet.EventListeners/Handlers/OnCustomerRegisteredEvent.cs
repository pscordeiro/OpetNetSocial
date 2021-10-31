using AutoMapper;
using OpetNet.Application.Interfaces;
using OpetNet.Domain.Commands;
using OpetNet.Domain.Core.Bus;
using OpetNet.Domain.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SharedKernel.DomainEvents.Core;
using System;

namespace OpetNet.EventListeners.Handlers
{
    public class OnCustomerRegisteredEvent : IHandler<CustomerRegisteredEvent>
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;

        public OnCustomerRegisteredEvent(ILogger logger, IServiceProvider serviceProvider, IMapper mapper)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        public void Handle(CustomerRegisteredEvent customerRegisteredEvent)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var bus = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                    var map = _mapper.Map<RegisterNewCustomerCommand>(customerRegisteredEvent);
                    bus.SendCommand(map).Wait();
                }
            }
            catch (Exception ex)
            {
                if (_logger.IsEnabled(LogLevel.Error)) { _logger.LogError($"ERROR - EventsListenerCommand - {ex.Message}"); }
                throw;
            }
        }
    }
}
