using Microsoft.Extensions.DependencyInjection;
using Transferencia.Domain.Core.Interfaces;
using Transferencia.Domain.Repositories;
using Transferencia.Infra.Data.Repositories;

namespace Transferencia.Infra.CrossCutting.IoC
{
    public static class RepositoriesExtention
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}
