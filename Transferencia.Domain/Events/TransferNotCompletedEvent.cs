using MediatR;
using System;

namespace Transferencia.Domain.Events
{
    public class TransferNotCompletedEvent : INotification
    {
        public TransferNotCompletedEvent(Guid transactionId, string errorMessage)
        {
            TransactionId = transactionId;
            ErrorMessage = errorMessage;
        }

        public Guid TransactionId { get; private set; }
        public string ErrorMessage { get; private set; }
    }
}
