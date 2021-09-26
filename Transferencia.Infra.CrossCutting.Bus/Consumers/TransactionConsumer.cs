using MassTransit;
using System.Threading.Tasks;
using Transferencia.Domain.Aggregates;
using Transferencia.Domain.Core.Interfaces;
using Transferencia.Domain.Repositories;
using Transferencia.Domain.Services;

namespace Transferencia.Infra.CrossCutting.Bus.Consumers
{
    public class TransactionConsumer : IConsumer<Transaction>
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionConsumer(ITransactionService transactionService, ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            _transactionService = transactionService;
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;            
        }

        public async Task Consume(ConsumeContext<Transaction> context)
        {
            var transaction = await _transactionRepository.GetByIdAsync(context.Message.Id);

            transaction.SetProcessing();            
            await _unitOfWork.CommitAsync();

            await _transactionService.PerformTransaction(transaction);
        }
    }
}
