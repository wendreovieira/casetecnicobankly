using System;
using Transferencia.Domain.Core.Aggregates;
using Transferencia.Domain.Enums;

namespace Transferencia.Domain.Aggregates
{
    public sealed class Transaction : Entity
    {
        public Transaction(string accountOrigin, string accountDestination, float value)
        {
            if (String.IsNullOrWhiteSpace(accountOrigin) || String.IsNullOrWhiteSpace(accountDestination))
                throw new ArgumentNullException();

            if (value <= 0)
                throw new ArgumentOutOfRangeException();

            AccountOrigin = accountOrigin;
            AccountDestination = accountDestination;
            Value = value;
            Status = ETransactionStatus.InQueue;
            ErrorMessage = "";
            DebitCompleted = false;
            ReverseTransferCompleted = false;
        }

        public string AccountOrigin { get; private set; }
        public string AccountDestination { get; private set; }
        public float Value { get; private set; }
        public ETransactionStatus Status { get; private set; }        
        public string ErrorMessage { get; private set; }
        public bool DebitCompleted { get; private set; }
        public bool ReverseTransferCompleted { get; private set; }

        public void SetError(string error)
        {
            Status = ETransactionStatus.Error;            
            ErrorMessage = error;
        }

        public void SetProcessing()
        {
            Status = ETransactionStatus.Processing;
        }

        public void SetConfirmed()
        {
            Status = ETransactionStatus.Confirmed;
        }

        public void CompleteDebit()
        {
            DebitCompleted = true;
        }

        public void CompleteReverseTransfer()
        {
            ReverseTransferCompleted = true;
        }
    }
}
