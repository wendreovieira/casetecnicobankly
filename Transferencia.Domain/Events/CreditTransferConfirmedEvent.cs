using MediatR;
using System;

namespace Transferencia.Domain.Events
{
    public class CreditTransferConfirmedEvent : INotification
    {
        public CreditTransferConfirmedEvent(Guid transactionId)
        {
            TransactionId = transactionId;
        }

        public Guid TransactionId { get; private set; }
    }
}
