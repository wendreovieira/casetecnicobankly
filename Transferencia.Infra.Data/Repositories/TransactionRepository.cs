using Transferencia.Domain.Aggregates;
using Transferencia.Domain.Repositories;
using Transferencia.Infra.Data.Contexts;

namespace Transferencia.Infra.Data.Repositories
{
    public class TransactionRepository : Repository<DomainContext, Transaction>, ITransactionRepository
    {                
        public TransactionRepository(DomainContext context) : base(context)
        {            
        }        
    }
}
