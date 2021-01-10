using System;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using AlbedoTeam.Sdk.MessageConsumer.Configuration;
using AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Consumers;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Contracts.Requests;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Db;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Mappers;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Services;
using GreenPipes;
using MassTransit;
using MassTransit.Audit;
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

            if (brokerConfiguration.UseEventStore)
            {
                services.AddScoped<IMessageAuditStore, MongoMessageAuditStore>();
                services.AddScoped<IEventStoreRepository, EventStoreRepository>();
                services.AddScoped<IMessageMapper, MessageMapper>();
                services.AddScoped<IEventStoreService, EventStoreService>();

                var dbSettings = provider.GetService<IDbSettings>();
                if (dbSettings == null)
                    throw new InvalidOperationException("Please add Data Access Layer to start Event Store");
            }

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

                    cfg.Host(brokerConfiguration.Options.Host);
                    cfg.ConfigureEndpoints(context);

                    var prd = services.BuildServiceProvider();
                    var auditStore = prd.GetService<IMessageAuditStore>();

                    if (brokerConfiguration.UseEventStore)
                        cfg.ConnectSendAuditObservers(auditStore);
                });

                services.AddSingleton(configure);
                services.AddScoped<IConsumerRegistration, ConsumerRegistration>();
                services.AddScoped<IDestinationQueueMapper, DestinationQueueMapper>();
                services.AddScoped<IRequestClientRegistration, RequestClientRegistration>();

                provider = services.BuildServiceProvider();

                var consumerRegistration = provider.GetService<IConsumerRegistration>();
                configureConsumers?.Invoke(consumerRegistration);

                if (brokerConfiguration.UseEventStore)
                    consumerRegistration.Add<EventRedeliveryRequestConsumer>();

                var destinationQueueMapper = provider.GetService<IDestinationQueueMapper>();
                configureDestinationQueues?.Invoke(destinationQueueMapper);

                var requestClientRegistration = provider.GetService<IRequestClientRegistration>();
                configureRequestClients?.Invoke(requestClientRegistration);

                // if (brokerConfiguration.UseEventStore)
                requestClientRegistration.Add<EventRedeliveryRequest>();
            });

            return services;
        }
    }
}