using System.Threading.Tasks;
using Transferencia.Domain.Aggregates;
using Transferencia.Domain.Enums;

namespace Transferencia.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetByAccountNumber(string accountNumber);
        Task Transfer(string accountNumber, float value, ETransferType type);
    }
}
