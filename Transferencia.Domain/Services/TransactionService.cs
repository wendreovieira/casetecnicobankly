using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Transferencia.Domain.Aggregates;
using Transferencia.Domain.Core.Message;
using Transferencia.Domain.Enums;
using Transferencia.Domain.Events;
using Transferencia.Domain.Exceptions;
using Transferencia.Domain.Repositories;

namespace Transferencia.Domain.Services
{
    public sealed class TransactionService : ITransactionService
    {
        private readonly ILogger _logger;                
        private readonly IAccountRepository _accountRepository;
        private readonly IMediator _mediator;

        public TransactionService(ILogger logger, IMediator mediator, IAccountRepository accountRepository)
        {
            _logger = logger;            
            _mediator = mediator;
            _accountRepository = accountRepository;
        }

        public async Task PerformTransaction(Transaction transaction)
        {
            try
            {
                var originAccount = await _accountRepository.GetByAccountNumber(transaction.AccountOrigin);
                var destinationAccount = await _accountRepository.GetByAccountNumber(transaction.AccountDestination);

                if (originAccount.Balance < transaction.Value)
                    throw new AccountDoesNotHaveEnoughBalanceException(DefaultMessages.AccountDoesNotHaveEnoughBalance);

                await _accountRepository.Transfer(originAccount.AccountNumber, transaction.Value, ETransferType.Debit);
                _ = _mediator.Publish(new DebitTransferConfirmedEvent(transaction.Id));                

                await _accountRepository.Transfer(destinationAccount.AccountNumber, transaction.Value, ETransferType.Credit);
                _ = _mediator.Publish(new CreditTransferConfirmedEvent(transaction.Id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _ = _mediator.Publish(new TransferNotCompletedEvent(transaction.Id, ex.Message));                
            }
        }

        public async Task PerformReverseTransfer(ReverseTransfer reverseTransfer)
        {
            try
            {
                var account = await _accountRepository.GetByAccountNumber(reverseTransfer.AccountNumber);

                await _accountRepository.Transfer(account.AccountNumber, reverseTransfer.Value, ETransferType.Credit);
                await _mediator.Publish(new ReverseTransferConfirmedEvent(reverseTransfer.TransactionId));
            }
            catch (Exception ex)
            {
                _logger.LogError(DefaultMessages.ReverseTransferNotCompleted(ex.Message));
                throw;
            }
        }
    }
}
