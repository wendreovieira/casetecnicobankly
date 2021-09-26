using FluentValidation.Results;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Transferencia.Domain.Core.Message
{
    public abstract class Command<TResponse> : IRequest<TResponse>
    {
        protected Command()
        {
            Timestamp = DateTime.Now;
        }
        
        public DateTime Timestamp { get; private set; }
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
