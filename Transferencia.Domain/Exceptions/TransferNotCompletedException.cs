using System;

namespace Transferencia.Domain.Exceptions
{
    public class TransferNotCompletedException : Exception
    {
        public TransferNotCompletedException()
        {
        }

        public TransferNotCompletedException(string message)
            : base(message)
        {            
        }
    }
}
