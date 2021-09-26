using MediatR;
using System;

namespace Transferencia.Domain.Events
{
    public class ReverseTransferConfirmedEvent : INotification
    {
        public ReverseTransferConfirmedEvent(Guid transactionId)
        {
            TransactionId = transactionId;
        }

        public Guid TransactionId { get; private set; }
    }
}
