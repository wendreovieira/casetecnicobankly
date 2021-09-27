using System;
using Transferencia.Domain.Aggregates;
using Xunit;

namespace Transferencia.Tests.Aggregates
{
    public class AccountTests
    {
        private readonly int _id = 1;
        private readonly float _balance = 1;
        private readonly string _account = "123";

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void When_not_inform_account(string account)
        {
            Assert.Throws<ArgumentNullException>(() => new Account(_id, account, _balance));
        }        

        [Fact]
        public void When_create_new_transaction()
        {
            var account = new Account(_id, _account, _balance);
            Assert.Equal(_id, account.Id);
            Assert.Equal(_account, account.AccountNumber);
            Assert.Equal(_balance, account.Balance);            
        }
    }
}
