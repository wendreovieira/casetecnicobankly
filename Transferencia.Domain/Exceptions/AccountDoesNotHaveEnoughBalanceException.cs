using System;

namespace Transferencia.Domain.Exceptions
{
    public class AccountDoesNotHaveEnoughBalanceException : Exception
    {
        public AccountDoesNotHaveEnoughBalanceException()
        {
        }

        public AccountDoesNotHaveEnoughBalanceException(string message)
            : base(message)
        {
        }
    }
}
