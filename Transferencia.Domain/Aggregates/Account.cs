using Newtonsoft.Json;
using System;

namespace Transferencia.Domain.Aggregates
{
    public sealed class Account
    {
        public Account(int id, string accountNumber, float balance)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentNullException();
            
            Id = id;
            AccountNumber = accountNumber;
            Balance = balance;
        }

        [JsonProperty]
        public int Id { get; private set; }
        [JsonProperty]
        public string AccountNumber { get; private set; }
        [JsonProperty]
        public float Balance { get; private set; }
    }
}
