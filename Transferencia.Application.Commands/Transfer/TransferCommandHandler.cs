using FluentValidation;
using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Transferencia.Domain.Aggregates;
using Transferencia.Domain.Core.Interfaces;
using Transferencia.Domain.Repositories;

namespace Transferencia.Application.Commands.Transfer
{
    public class TransferCommandHandler : IRequestHandler<TransferCommand, TransferCommandResult>
    {        
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBusControl _bus;

        public TransferCommandHandler(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IBusControl bus)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _bus = bus;
        }

        public async Task<TransferCommandResult> Handle(TransferCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) throw new ValidationException(request.ValidationResult.Errors);

            var transaction = new Transaction(request.AccountOrigin, request.AccountDestination, request.Value);
            
            _transactionRepository.Add(transaction);
            await _unitOfWork.CommitAsync();

            await _bus.Publish(transaction);

            return new TransferCommandResult
            {
                TransactionId = transaction.Id
            };
        }
    }
}
