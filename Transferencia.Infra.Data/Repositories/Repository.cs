using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Transferencia.Domain.Core.Aggregates;
using Transferencia.Domain.Core.Interfaces;

namespace Transferencia.Infra.Data.Repositories
{
    public abstract class Repository<TContext, TEntity> : IRepository<TEntity>
    where TContext : DbContext
    where TEntity : Entity
    {
        private readonly TContext _context;        

        public Repository(TContext context)
        {
            _context = context;            
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public void Add(TEntity entity)
        {            
            _context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {            
            var local = _context.Set<TEntity>().Local.Where(x => x.Id == entity.Id).FirstOrDefault();

            if (local != null)
                _context.Entry(local).State = EntityState.Detached;

            SetModifiedState(entity);
        }                

        private void SetModifiedState(TEntity entity)
        {   
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
