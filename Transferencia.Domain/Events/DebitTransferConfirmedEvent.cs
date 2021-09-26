using MediatR;
using System;

namespace Transferencia.Domain.Events
{
    public class DebitTransferConfirmedEvent : INotification
    {
        public DebitTransferConfirmedEvent(Guid transactionId)
        {
            TransactionId = transactionId;
        }

        public Guid TransactionId { get; private set; }
    }
}
