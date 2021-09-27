using System;
using Transferencia.Domain.Aggregates;
using Xunit;

namespace Transferencia.Tests.Aggregates
{
    public class ReverseTransferTests
    {
        private readonly string _account = "123";
        private readonly float _value = 1;
        private readonly Guid _transactionId = Guid.NewGuid();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void When_not_inform_account(string account)
        {
            Assert.Throws<ArgumentNullException>(() => new ReverseTransfer(account, _value, _transactionId));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]        
        public void When_value_is_invalid(float value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ReverseTransfer(_account, value, _transactionId));
        }

        [Fact]
        public void When_not_inform_transactionId()
        {
            Assert.Throws<ArgumentNullException>(() => new ReverseTransfer(_account, _value, Guid.Empty));
        }

        [Fact]
        public void When_create_new_transaction()
        {
            var reverseTransfer = new ReverseTransfer(_account, _value, _transactionId);
            Assert.Equal(_account, reverseTransfer.AccountNumber);
            Assert.Equal(_value, reverseTransfer.Value);
            Assert.Equal(_transactionId, reverseTransfer.TransactionId);
        }
    }
}
