using System.Threading.Tasks;
using OpetNet.Domain.Core.Bus;
using OpetNet.Domain.Core.Commands;
using OpetNet.Domain.Core.Events;
using MediatR;

namespace OpetNet.Infra.CrossCutting.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResponse> SendCommand<T, TResponse>(T command) where T : Command<TResponse>
        {
            return await _mediator.Send<TResponse>(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }

        public async Task SendCommand<TRequest>(TRequest command) where TRequest : Command
        {
            await _mediator.Send(command);
        }
    }
}
