using System.Threading;
using System.Threading.Tasks;
using OpetNet.Domain.Events;
using MediatR;
using SharedKernel.DomainEvents.Core;
using SharedKernel.DomainEvents.CrossDomainEvents;

namespace OpetNet.Domain.EventHandlers
{
    public class CustomerEventHandler :
        EventHandler,
        INotificationHandler<CustomerRegisteredEvent>,
        INotificationHandler<CustomerUpdatedEvent>,
        INotificationHandler<CustomerRemovedEvent>
    {
        public CustomerEventHandler(IHandler<ICrossDomainEvent> crossDomainEventHandler) : base(crossDomainEventHandler)
        {
        }

        public Task Handle(CustomerRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail
            HandleCrossDomainEvents(message);

            return Task.CompletedTask;
        }

        public Task Handle(CustomerRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail
            HandleCrossDomainEvents(message);

            return Task.CompletedTask;
        }

        public Task Handle(CustomerUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail
            HandleCrossDomainEvents(message);

            return Task.CompletedTask;
        }
    }
}
