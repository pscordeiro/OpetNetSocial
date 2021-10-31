using System;
using System.Threading;
using System.Threading.Tasks;
using OpetNet.Domain.Commands;
using OpetNet.Domain.Core.Bus;
using OpetNet.Domain.Core.Notifications;
using OpetNet.Domain.Events;
using OpetNet.Domain.Interfaces;
using OpetNet.Domain.Models;
using MediatR;

namespace OpetNet.Domain.CommandHandlers
{
    public class CustomerCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewCustomerCommand, Unit>,
        IRequestHandler<UpdateCustomerCommand, Unit>,
        IRequestHandler<RemoveCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediatorHandler _bus;

        public CustomerCommandHandler(ICustomerRepository customerRepository, 
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _customerRepository = customerRepository;
            _bus = bus;
        }

        public async Task<Unit> Handle(RegisterNewCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Unit.Value;
            }

            var customer = new Customer(Guid.NewGuid(), message.Name, message.Email, message.BirthDate);

            if (_customerRepository.GetByEmail(customer.Email) != null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "The customer e-mail has already been taken."));
            }

            if (Commit())
            {
               await _bus.RaiseEvent(new CustomerRegisteredEvent(customer.Id, customer.Name, customer.Email, customer.BirthDate));
            }

            return Unit.Value;
        }

        public Task<Unit> Handle(UpdateCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Unit.Task;
            }

            var customer = new Customer(message.Id, message.Name, message.Email, message.BirthDate);
            var existingCustomer = _customerRepository.GetByEmail(customer.Email);

            if (existingCustomer != null && existingCustomer.Id != customer.Id)
            {
                if (!existingCustomer.Equals(customer))
                {
                    _bus.RaiseEvent(new DomainNotification(message.MessageType,"The customer e-mail has already been taken."));
                    return Unit.Task;
                }
            }

            _customerRepository.Update(customer);

            if (Commit())
            {
                _bus.RaiseEvent(new CustomerUpdatedEvent(customer.Id, customer.Name, customer.Email, customer.BirthDate));
            }

            return Unit.Task;
        }

        public Task<Unit> Handle(RemoveCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Unit.Task;
            }

            _customerRepository.Remove(message.Id);

            if (Commit())
            {
                _bus.RaiseEvent(new CustomerRemovedEvent(message.Id));
            }

            return Unit.Task;
        }

    }
}
