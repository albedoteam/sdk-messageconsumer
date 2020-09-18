using System;
using GreenPipes;
using MassTransit;
using MessageConsumerSdk.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageConsumerSdk
{
    public static class ConsumerExtensions
    {
        public static IServiceCollection AddMessageBrokerConsumer(
            this IServiceCollection services,
            IConsumerOptions options,
            Action<IConsumerRegistration> consumers,
            Action<IQueueMapper> queues = null)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.BrokerHost))
                throw new InvalidOperationException("Can not start the service without a valid Message Broker Host");

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseMessageRetry(r =>
                    {
                        r.Intervals(3000, 15000, 30000);
                        r.Ignore<ArgumentNullException>();
                    });

                    cfg.Host(options.BrokerHost);
                    cfg.ConfigureEndpoints(context);
                });

                services.AddSingleton(x);
                services.AddScoped<IConsumerRegistration, ConsumerRegistration>();
                services.AddScoped<IQueueMapper, QueueMapper>();

                var provider = services.BuildServiceProvider();

                var cr = provider.GetService<IConsumerRegistration>();
                consumers.Invoke(cr);

                var qm = provider.GetService<IQueueMapper>();
                queues?.Invoke(qm);
            });
            
            return services;
        }
    }
}