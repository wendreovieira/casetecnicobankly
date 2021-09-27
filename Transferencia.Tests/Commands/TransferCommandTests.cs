using Transferencia.Application.Commands.Transfer;
using Xunit;

namespace Transferencia.Tests.Commands
{
    public class TransferCommandTests
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
            var command = new TransferCommand(originAccount, _destinationAccount, _value);
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void When_not_inform_destination_account(string destinationAccount)
        {
            var command = new TransferCommand(_originAccount, destinationAccount, _value);
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void When_value_is_invalid(float value)
        {
            var command = new TransferCommand(_originAccount, _destinationAccount, value);
            Assert.False(command.IsValid());
        }

        [Fact]
        public void When_create_new_transferCommand()
        {
            var command = new TransferCommand(_originAccount, _destinationAccount, _value);
            Assert.Equal(_originAccount, command.AccountOrigin);
            Assert.Equal(_destinationAccount, command.AccountDestination);
            Assert.Equal(_value, command.Value);
        }
    }
}
