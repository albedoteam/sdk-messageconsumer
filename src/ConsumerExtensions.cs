using System;
using AlbedoTeam.Sdk.MessageConsumer.Configuration;
using AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace AlbedoTeam.Sdk.MessageConsumer
{
    public static class ConsumerExtensions
    {
        public static IServiceCollection AddBroker(
            this IServiceCollection services,
            Action<IBrokerConfigurator> configureBroker,
            Action<IConsumerRegistration> configureConsumers)
        {
            return services.AddBrokerHub(configureBroker, configureConsumers);
        }

        public static IServiceCollection AddBroker(
            this IServiceCollection services,
            Action<IBrokerConfigurator> configureBroker,
            Action<IConsumerRegistration> configureConsumers,
            Action<IDestinationQueueMapper> configureDestinationQueues)
        {
            return services.AddBrokerHub(configureBroker, configureConsumers, configureDestinationQueues);
        }

        public static IServiceCollection AddBroker(
            this IServiceCollection services,
            Action<IBrokerConfigurator> configureBroker,
            Action<IRequestClientRegistration> configureRequestClients)
        {
            return services.AddBrokerHub(configureBroker, null, null, configureRequestClients);
        }

        public static IServiceCollection AddBroker(
            this IServiceCollection services,
            Action<IBrokerConfigurator> configureBroker,
            Action<IConsumerRegistration> configureConsumers,
            Action<IDestinationQueueMapper> configureDestinationQueues,
            Action<IRequestClientRegistration> configureRequestClients)
        {
            return services.AddBrokerHub(configureBroker, configureConsumers, configureDestinationQueues,
                configureRequestClients);
        }

        private static IServiceCollection AddBrokerHub(
            this IServiceCollection services,
            Action<IBrokerConfigurator> configureBroker,
            Action<IConsumerRegistration> configureConsumers = null,
            Action<IDestinationQueueMapper> configureDestinationQueues = null,
            Action<IRequestClientRegistration> configureRequestClients = null)
        {
            if (configureBroker == null)
                throw new ArgumentNullException(nameof(configureBroker));

            services.AddScoped<IBrokerConfigurator, BrokerConfigurator>();

            var provider = services.BuildServiceProvider();

            var brokerConfiguration = provider.GetService<IBrokerConfigurator>();
            configureBroker.Invoke(brokerConfiguration);

            services.AddSingleton(brokerConfiguration.Options);
            services.AddTransient<IBusRunner, BusRunner>();

            services.AddMassTransit(configure =>
            {
                configure.SetKebabCaseEndpointNameFormatter();
                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(brokerConfiguration.Options.Host);
                    cfg.ConfigureEndpoints(context);
                });

                services.AddSingleton(configure);
                services.AddScoped<IConsumerRegistration, ConsumerRegistration>();
                services.AddScoped<IDestinationQueueMapper, DestinationQueueMapper>();
                services.AddScoped<IRequestClientRegistration, RequestClientRegistration>();

                provider = services.BuildServiceProvider();

                var consumerRegistration = provider.GetService<IConsumerRegistration>();
                configureConsumers?.Invoke(consumerRegistration);

                var destinationQueueMapper = provider.GetService<IDestinationQueueMapper>();
                configureDestinationQueues?.Invoke(destinationQueueMapper);

                var requestClientRegistration = provider.GetService<IRequestClientRegistration>();
                configureRequestClients?.Invoke(requestClientRegistration);
            });

            return services;
        }
    }
}