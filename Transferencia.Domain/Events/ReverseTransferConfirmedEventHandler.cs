using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Transferencia.Domain.Core.Interfaces;
using Transferencia.Domain.Repositories;

namespace Transferencia.Domain.Events
{
    public class ReverseTransferConfirmedEventHandler : INotificationHandler<ReverseTransferConfirmedEvent>
    {        
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReverseTransferConfirmedEventHandler(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ReverseTransferConfirmedEvent notification, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(notification.TransactionId);

            transaction.CompleteReverseTransfer();
            await _unitOfWork.CommitAsync();
        }
    }
}
