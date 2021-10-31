using OpetNet.Domain.Core.Bus;
using OpetNet.Domain.Core.Commands;
using OpetNet.Domain.Core.Notifications;
using OpetNet.Domain.Interfaces;
using MediatR;

namespace OpetNet.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            _bus = bus;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotification(error.ErrorCode ?? message.MessageType, $"{message.MessageType} : {error.ErrorMessage}"));
            }
        }

        protected void NotifyValidationErrors<TResponse>(Command<TResponse> message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotification(error.ErrorCode ?? message.MessageType, $"{message.MessageType} : {error.ErrorMessage}"));
            }
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) return false;

            if (_uow.Commit())
            {
                return true;
            }

            _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));

            return false;
        }
    }
}
