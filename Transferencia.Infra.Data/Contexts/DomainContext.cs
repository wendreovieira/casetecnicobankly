using Microsoft.EntityFrameworkCore;
using Transferencia.Domain.Aggregates;

namespace Transferencia.Infra.Data.Contexts
{
    public class DomainContext : DbContext
    {                
        public DomainContext(DbContextOptions<DomainContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
