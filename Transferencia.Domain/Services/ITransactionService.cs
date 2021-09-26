using System.Threading.Tasks;
using Transferencia.Domain.Aggregates;

namespace Transferencia.Domain.Services
{
    public interface ITransactionService
    {
        Task PerformTransaction(Transaction transaction);
        Task PerformReverseTransfer(ReverseTransfer reverseTransfer);
    }
}
