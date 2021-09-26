using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Transferencia.Domain.Core.Interfaces;
using Transferencia.Domain.Repositories;

namespace Transferencia.Domain.Events
{
    public class CreditTransferConfirmedEventHandler : INotificationHandler<CreditTransferConfirmedEvent>
    {        
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreditTransferConfirmedEventHandler(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreditTransferConfirmedEvent notification, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(notification.TransactionId);

            transaction.SetConfirmed();
            await _unitOfWork.CommitAsync();
        }
    }
}
