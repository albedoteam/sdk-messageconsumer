using System;
using AlbedoTeam.Sdk.MessageConsumer.Configuration;
using AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AlbedoTeam.Sdk.MessageConsumer
{
    public static class ConsumerExtensions
    {
        public static IServiceCollection AddConsumer(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<IConsumerRegistration> consumers = null,
            Action<IDestinationQueueMapper> queues = null)
        {
            services.Configure<MessageBrokerOptions>(configuration.GetSection(nameof(MessageBrokerOptions)));
            services.AddSingleton<IMessageBrokerOptions>(provider =>
                provider.GetRequiredService<IOptions<MessageBrokerOptions>>().Value);

            services.AddTransient<IBusRunner, BusRunner>();

            var sp = services.BuildServiceProvider();
            var options = sp.GetService<IMessageBrokerOptions>();

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.Host))
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

                    cfg.Host(options.Host);
                    cfg.ConfigureEndpoints(context);
                });

                services.AddSingleton(x);
                services.AddScoped<IConsumerRegistration, ConsumerRegistration>();
                services.AddScoped<IDestinationQueueMapper, DestinationQueueMapper>();

                var provider = services.BuildServiceProvider();

                var cr = provider.GetService<IConsumerRegistration>();
                consumers?.Invoke(cr);

                var qm = provider.GetService<IDestinationQueueMapper>();
                queues?.Invoke(qm);
            });

            return services;
        }
    }
}