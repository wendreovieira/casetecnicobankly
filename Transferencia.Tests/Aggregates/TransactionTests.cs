using System;
using Transferencia.Domain.Aggregates;
using Transferencia.Domain.Enums;
using Xunit;

namespace Transferencia.Tests.Aggregates
{
    public class TransactionTests
    {
        private readonly string _originAccount = "123";        
        private readonly string _destinationAccount = "321";
        private readonly float _value = 1;

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void When_not_inform_origin_account(string originAccount)
        {
            Assert.Throws<ArgumentNullException>(() => new Transaction(originAccount, _destinationAccount, _value));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void When_not_inform_destination_account(string destinationAccount)
        {
            Assert.Throws<ArgumentNullException>(() => new Transaction(_originAccount, destinationAccount, _value));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]        
        public void When_value_is_invalid(float value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Transaction(_originAccount, _destinationAccount, value));
        }

        [Fact]
        public void When_create_new_transaction()
        {
            var transaction = new Transaction(_originAccount, _destinationAccount, _value);
            Assert.Equal(_originAccount, transaction.AccountOrigin);
            Assert.Equal(_destinationAccount, transaction.AccountDestination);
            Assert.Equal(_value, transaction.Value);
            Assert.Equal(ETransactionStatus.InQueue, transaction.Status);            
            Assert.False(transaction.DebitCompleted);
            Assert.False(transaction.ReverseTransferCompleted);
        }
    }
}
