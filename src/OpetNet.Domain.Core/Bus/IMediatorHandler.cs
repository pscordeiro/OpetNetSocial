using System.Threading.Tasks;
using OpetNet.Domain.Core.Commands;
using OpetNet.Domain.Core.Events;

namespace OpetNet.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<TRequest>(TRequest command) where TRequest : Command;
        Task<TResponse> SendCommand<TRequest, TResponse>(TRequest command) where TRequest : Command<TResponse>;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
