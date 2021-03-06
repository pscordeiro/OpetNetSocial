using System;
using OpetNet.Domain.Core.Events;
using FluentValidation.Results;
using MediatR;

namespace OpetNet.Domain.Core.Commands
{
    public abstract class Command<TResponse> : Message, IRequest<TResponse>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }

    public abstract class Command : Message
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
