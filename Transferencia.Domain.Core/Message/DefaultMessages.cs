using System;

namespace Transferencia.Domain.Core.Message
{
    public static class DefaultMessages
    {
        public static string AccountDoesNotHaveEnoughBalance = "The origin account does not have enough balance for this transfer.";
        public static string UnexpectedAccountApiError(string error) => $"An unexpected error occurred while connecting to the accounts API: {error}";
        public static string TransferNotCompleted(string error) => $"Transfer not completed: {error}";
        public static string ReverseTransferNotCompleted(string error) => $"Reverse transfer not completed: {error}";
        public static string StartingTransaction(Guid id) => $"Starting new transaction: {id}";
        public static string TransactionConfirmed(Guid id) => $"Transaction confirmed: {id}";
        public static string AccountNotFound(string accountNumber) => $"Account {accountNumber} not found.";
    }
}
