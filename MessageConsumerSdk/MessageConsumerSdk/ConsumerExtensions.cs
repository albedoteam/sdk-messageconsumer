using System;
using System.Collections.Generic;
using GreenPipes;
using MassTransit;
using MessageConsumerSdk.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MessageConsumerSdk
{
    public interface IBroker
    {
        public string Host { get; set; }
        public Dictionary<string, string> DestinationQueues { get; set; }
    }

    public class Broker : IBroker
    {
        public string Host { get; set; }
        public Dictionary<string, string> DestinationQueues { get; set; }
    }

    public static class ConsumerExtensions
    {
        public static IServiceCollection AddMessageBrokerConsumer(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<IConsumerRegistration> consumers,
            Action<IDestinationQueueMapper> queues = null)
        {
            var sp = services.BuildServiceProvider();
            var options = sp.GetService<IBroker>();

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.Host))
                throw new InvalidOperationException("Can not start the service without a valid Message Broker Host");

            services.AddSingleton<IBroker>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<Broker>>().Value);

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

                    cfg.Host(options.Host);
                    cfg.ConfigureEndpoints(context);
                });

                services.AddSingleton(x);
                services.AddScoped<IConsumerRegistration, ConsumerRegistration>();
                services.AddScoped<IDestinationQueueMapper, DestinationQueueMapper>();

                var provider = services.BuildServiceProvider();

                var cr = provider.GetService<IConsumerRegistration>();
                consumers.Invoke(cr);

                var qm = provider.GetService<IDestinationQueueMapper>();
                queues?.Invoke(qm);
            });

            return services;
        }
    }
}