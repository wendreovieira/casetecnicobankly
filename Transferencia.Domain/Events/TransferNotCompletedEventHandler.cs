using MassTransit;
using MediatR;
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

        public TransferNotCompletedEventHandler(IBusControl bus, ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            _bus = bus;
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(TransferNotCompletedEvent notification, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(notification.TransactionId);

            if (transaction.DebitCompleted)
            {
                var reverseTransfer = new ReverseTransfer(transaction.AccountOrigin, transaction.Value, transaction.Id);
                await _bus.Publish(reverseTransfer);
            }

            transaction.SetError(notification.ErrorMessage);
            await _unitOfWork.CommitAsync();            
        }
    }
}
