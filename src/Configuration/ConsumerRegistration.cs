using System;
using AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions;
using GreenPipes.Configurators;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;

namespace AlbedoTeam.Sdk.MessageConsumer.Configuration
{
    public class ConsumerRegistration : IConsumerRegistration
    {
        private readonly IServiceCollectionBusConfigurator _busConfigurator;

        public ConsumerRegistration(IServiceCollectionBusConfigurator busConfigurator)
        {
            _busConfigurator = busConfigurator;
        }

        public void Add<T>(Action<IRetryConfigurator> configureRetries = null) where T : class, IConsumer
        {
            if (configureRetries != null)
                _busConfigurator.AddConsumer<T>(c =>
                {
                    c.UseInMemoryOutbox();
                    c.UseMessageRetry(configureRetries.Invoke);
                });
            else
                _busConfigurator.AddConsumer<T>(c => { c.UseInMemoryOutbox(); });
        }

        public void AddRequestClient<T>() where T : class
        {
            _busConfigurator.AddRequestClient<T>();
        }
    }
}