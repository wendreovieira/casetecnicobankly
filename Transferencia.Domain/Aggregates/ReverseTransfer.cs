using System;

namespace Transferencia.Domain.Aggregates
{
    public class ReverseTransfer
    {
        public ReverseTransfer(string accountNumber, float value, Guid transactionId)
        {
            if (String.IsNullOrWhiteSpace(accountNumber) || transactionId == Guid.Empty)
                throw new ArgumentNullException();

            if (value <= 0)
                throw new ArgumentOutOfRangeException();

            AccountNumber = accountNumber;
            Value = value;
            TransactionId = transactionId;
        }

        public Guid TransactionId { get; private set; }
        public string AccountNumber { get; private set; }
        public float Value { get; private set; }
    }
}
