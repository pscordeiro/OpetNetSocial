using OpetNet.Domain.Core.Events;

namespace OpetNet.Domain.Core.Notifications
{
    public class DomainNotification : Event
    {
        public DomainNotification(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }

        public string Message { get; }
    }
}
