using System;
using AlbedoTeam.Sdk.MessageConsumer.Configuration;
using AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace AlbedoTeam.Sdk.MessageConsumer
{
    public static class ConsumerExtensions
    {
        public static IServiceCollection AddConsumer(
            this IServiceCollection services,
            Action<IMessageBrokerOptions> configureBroker,
            Action<IConsumerRegistration> consumers = null,
            Action<IDestinationQueueMapper> queues = null)
        {
            if (configureBroker == null)
                throw new ArgumentNullException(nameof(configureBroker));
            
            IMessageBrokerOptions brokerOptions = new MessageBrokerOptions();
            configureBroker.Invoke(brokerOptions);
            
            if (brokerOptions == null)
                throw new NullReferenceException(nameof(brokerOptions));
            
            if (string.IsNullOrWhiteSpace(brokerOptions.Host))
                throw new InvalidOperationException("Can not start the service without a valid Message Broker Host");
            
            services.AddSingleton(brokerOptions);
            services.AddTransient<IBusRunner, BusRunner>();

            services.AddMassTransit(configure =>
            {
                configure.SetKebabCaseEndpointNameFormatter();
                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseMessageRetry(r =>
                    {
                        r.Intervals(3000, 15000, 30000);
                        r.Ignore<ArgumentNullException>();
                    });

                    cfg.Host(brokerOptions.Host);
                    cfg.ConfigureEndpoints(context);
                });

                services.AddSingleton(configure);
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