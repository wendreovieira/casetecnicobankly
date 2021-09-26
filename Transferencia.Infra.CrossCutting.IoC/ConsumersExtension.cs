using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using Transferencia.Infra.CrossCutting.Bus.Consumers;

namespace Transferencia.Infra.CrossCutting.IoC
{
    public static class ConsumersExtension
    {
        public static void AddConsumers(this IServiceCollection services)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<TransactionConsumer>();
                cfg.AddConsumer<ReverseTransferConsumer>();

                cfg.UsingInMemory((context, config) =>
                {
                    config.ReceiveEndpoint("transaction_queue", e =>
                    {
                        e.ConfigureConsumer<TransactionConsumer>(context);
                    });

                    config.ReceiveEndpoint("reverse_transfer_queue", e =>
                    {
                        e.UseMessageRetry(r =>
                        {
                            r.Interval(10, TimeSpan.FromSeconds(10));
                        });

                        e.ConfigureConsumer<ReverseTransferConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}
