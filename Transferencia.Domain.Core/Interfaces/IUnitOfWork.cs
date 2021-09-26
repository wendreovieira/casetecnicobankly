using System.Threading.Tasks;

namespace Transferencia.Domain.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}
