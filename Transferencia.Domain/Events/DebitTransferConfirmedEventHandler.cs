using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Transferencia.Domain.Core.Interfaces;
using Transferencia.Domain.Repositories;

namespace Transferencia.Domain.Events
{
    public class DebitTransferConfirmedEventHandler : INotificationHandler<DebitTransferConfirmedEvent>
    {        
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public DebitTransferConfirmedEventHandler(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, ILogger logger)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(DebitTransferConfirmedEvent notification, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(notification.TransactionId);

            transaction.CompleteDebit();
            await _unitOfWork.CommitAsync();

            _logger.LogInformation($"Confirmed debit on account: {transaction.AccountOrigin}");
        }
    }
}
