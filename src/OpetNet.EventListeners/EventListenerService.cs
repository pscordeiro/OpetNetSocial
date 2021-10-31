using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using OpetNet.Domain.Events;
using OpetNet.EventListeners.Handlers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharedKernel.DomainEvents.CrossDomainEvents.Interfaces;

namespace OpetNet.EventListeners
{
    public class EventListenerService : IHostedService, IDisposable
    {
        private readonly ILogger<EventListenerService> _logger;
        private readonly IListener _listener;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;

        public EventListenerService(ILogger<EventListenerService> logger, IListener listener, IServiceProvider serviceProvider, IMapper mapper)
        {
            _logger = logger;
            _listener = listener;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("OpetNet.EventListeners starting...");

            _listener.AddHandler(new OnCustomerRegisteredEvent(_logger, _serviceProvider, _mapper));


            _logger.LogInformation("OpetNet.EventListeners starded!");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("OpetNet.EventListeners stopping...");

            _listener.CloseConnections();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogInformation("OpetNet.EventListeners disposed...");
        }
    }
}
