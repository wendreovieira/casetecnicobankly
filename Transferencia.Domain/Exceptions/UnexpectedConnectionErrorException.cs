using System;

namespace Transferencia.Domain.Exceptions
{
    public class UnexpectedConnectionErrorException : Exception
    {
        public UnexpectedConnectionErrorException()
        {
        }

        public UnexpectedConnectionErrorException(string message)
            : base(message)
        {
        }
    }
}
