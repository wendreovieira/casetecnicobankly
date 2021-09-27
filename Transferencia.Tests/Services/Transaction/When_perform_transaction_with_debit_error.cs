using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using Transferencia.Domain.Aggregates;
using Transferencia.Domain.Enums;
using Transferencia.Domain.Events;
using Transferencia.Domain.Exceptions;
using Transferencia.Domain.Repositories;
using Transferencia.Domain.Services;
using Xunit;

namespace Transferencia.Tests.Services
{
    public class When_perform_transaction_with_debit_error
    {
        private readonly string _originAccountNumber = "123";
        private readonly string _destinationAccountNumber = "321";
        private readonly float _value = 1;
        private readonly Mock<ILogger> _logger = new Mock<ILogger>();
        private readonly Mock<IAccountRepository> _accountRepository = new Mock<IAccountRepository>();
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();
        private TransactionService _transactionService;
        private Transaction _transaction;

        public When_perform_transaction_with_debit_error()
        {
            _transaction = new Transaction(_originAccountNumber, _destinationAccountNumber, _value);            
            _mediator.Setup(x => x.Publish(It.IsAny<DebitTransferConfirmedEvent>(), It.IsAny<CancellationToken>()));
            _mediator.Setup(x => x.Publish(It.IsAny<CreditTransferConfirmedEvent>(), It.IsAny<CancellationToken>()));
            _accountRepository.Setup(x => x.GetByAccountNumber(_originAccountNumber)).ReturnsAsync(new Account(1, _originAccountNumber, 10));
            _accountRepository.Setup(x => x.GetByAccountNumber(_destinationAccountNumber)).ReturnsAsync(new Account(2, _destinationAccountNumber, 10));
            _accountRepository.Setup(x => x.Transfer(It.IsAny<string>(), It.IsAny<float>(), ETransferType.Debit)).Throws(new UnexpectedConnectionErrorException());            
            _transactionService = new TransactionService(_logger.Object, _mediator.Object, _accountRepository.Object);
        }

        [Fact]
        public async void Should_transfer()
        {
            await _transactionService.PerformTransaction(_transaction);
            _accountRepository.Verify(x => x.GetByAccountNumber(_originAccountNumber));
            _accountRepository.Verify(x => x.GetByAccountNumber(_destinationAccountNumber));
            _accountRepository.Verify(x => x.Transfer(_originAccountNumber, _value, ETransferType.Debit));            
            _mediator.Verify(x => x.Publish(It.IsAny<TransferNotCompletedEvent>(), It.IsAny<CancellationToken>()));
        }
    }
}
