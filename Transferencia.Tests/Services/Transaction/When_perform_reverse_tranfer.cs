using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using Transferencia.Domain.Aggregates;
using Transferencia.Domain.Enums;
using Transferencia.Domain.Events;
using Transferencia.Domain.Repositories;
using Transferencia.Domain.Services;
using Xunit;

namespace Transferencia.Tests.Services
{
    public class When_perform_reverse_transfer
    {
        private readonly string _accountNumber = "123";
        private readonly Guid _transactionId = Guid.NewGuid();
        private readonly float _value = 1;
        private readonly Mock<ILogger> _logger = new Mock<ILogger>();
        private readonly Mock<IAccountRepository> _accountRepository = new Mock<IAccountRepository>();
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();
        private TransactionService _transactionService;
        private ReverseTransfer _reverseTransfer;

        public When_perform_reverse_transfer()
        {
            _reverseTransfer = new ReverseTransfer(_accountNumber, _value, _transactionId);            
            _mediator.Setup(x => x.Publish(It.IsAny<ReverseTransferConfirmedEvent>(), It.IsAny<CancellationToken>()));            
            _accountRepository.Setup(x => x.GetByAccountNumber(_accountNumber)).ReturnsAsync(new Account(1, _accountNumber, 10));            
            _accountRepository.Setup(x => x.Transfer(It.IsAny<string>(), It.IsAny<float>(), ETransferType.Credit));
            _transactionService = new TransactionService(_logger.Object, _mediator.Object, _accountRepository.Object);
        }

        [Fact]
        public async void Should_transfer()
        {
            await _transactionService.PerformReverseTransfer(_reverseTransfer);
            _accountRepository.Verify(x => x.GetByAccountNumber(_accountNumber));                        
            _accountRepository.Verify(x => x.Transfer(_accountNumber, _value, ETransferType.Credit));
            _mediator.Verify(x => x.Publish(It.IsAny<ReverseTransferConfirmedEvent>(), It.IsAny<CancellationToken>()));            
        }
    }
}
