using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Transferencia.Infra.Data.Contexts;

namespace Transferencia.Application.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<DomainContext>(opt => 
                opt.UseNpgsql(configuration.GetConnectionString("DomainConnection")));
        }
    }
}
