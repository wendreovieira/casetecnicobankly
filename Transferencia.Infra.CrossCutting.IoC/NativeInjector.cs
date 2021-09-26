using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transferencia.Domain.Services;

namespace Transferencia.Infra.CrossCutting.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddConsumers();
            services.AddRepositories();
            services.AddScoped<ITransactionService, TransactionService>();            

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<ApplicationLogs>>();
            services.AddSingleton(typeof(ILogger), logger);
        }
    }
}
