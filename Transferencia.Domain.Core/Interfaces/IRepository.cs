using System;
using System.Threading.Tasks;
using Transferencia.Domain.Core.Aggregates;

namespace Transferencia.Domain.Core.Interfaces
{
    public interface IRepository<TEntity>
    where TEntity : Entity
    {
        void Add(TEntity entidade);
        void Update(TEntity entidade);
        Task<TEntity> GetByIdAsync(Guid id);
    }
}
