using Microsoft.Extensions.DependencyInjection;
using System;
using Transferencia.Infra.CrossCutting.IoC;

namespace Transferencia.Application.Configurations
{
    public static class DependecyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjector.RegisterServices(services);
        }
    }
}
