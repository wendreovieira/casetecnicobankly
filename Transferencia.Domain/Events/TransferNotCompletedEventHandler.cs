using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Transferencia.Domain.Aggregates;
using Transferencia.Domain.Core.Interfaces;
using Transferencia.Domain.Repositories;

namespace Transferencia.Domain.Events
{
    public class TransferNotCompletedEventHandler : INotificationHandler<TransferNotCompletedEvent>
    {
        private readonly IBusControl _bus;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public TransferNotCompletedEventHandler(IBusControl bus, ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, ILogger logger)
        {
            _bus = bus;
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(TransferNotCompletedEvent notification, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(notification.TransactionId);

            if (transaction.DebitCompleted)
            {
                _logger.LogInformation($"Credit tranfer not completed on account: {transaction.AccountDestination}");
                var reverseTransfer = new ReverseTransfer(transaction.AccountOrigin, transaction.Value, transaction.Id);
                await _bus.Publish(reverseTransfer);
            }
            else
            {
                _logger.LogInformation($"Debit tranfer not completed on account: {transaction.AccountOrigin}");
            }

            transaction.SetError(notification.ErrorMessage);
            await _unitOfWork.CommitAsync();            
        }
    }
}
