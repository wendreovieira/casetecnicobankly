using MassTransit;
using System.Threading.Tasks;
using Transferencia.Domain.Aggregates;
using Transferencia.Domain.Services;

namespace Transferencia.Infra.CrossCutting.Bus.Consumers
{
    public class ReverseTransferConsumer : IConsumer<ReverseTransfer>
    {
        private readonly ITransactionService _transactionService;

        public ReverseTransferConsumer(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task Consume(ConsumeContext<ReverseTransfer> context)
        {
            await _transactionService.PerformReverseTransfer(context.Message);
        }
    }
}
