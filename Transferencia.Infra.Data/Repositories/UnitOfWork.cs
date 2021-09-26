using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using Transferencia.Domain.Core.Interfaces;
using Transferencia.Infra.Data.Contexts;

namespace Transferencia.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DomainContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(DomainContext context)
        {
            _context = context;
            _transaction = _context.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();            
        }        

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }
    }
}
