using System;
using AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions;
using GreenPipes;
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

        public IConsumerRegistration Add<T>(Action<IRetryConfigurator> configureRetries = null)
            where T : class, IConsumer
        {
            if (configureRetries is null)
            {
                _busConfigurator.AddConsumer<T>(c =>
                {
                    c.UseInMemoryOutbox();
                    c.UseMessageRetry(r =>
                    {
                        r.Intervals(3000, 15000, 30000);
                        r.Ignore(
                            typeof(InvalidCastException),
                            typeof(InvalidOperationException),
                            typeof(ArgumentException),
                            typeof(ArgumentNullException));
                    });
                });
            }
            else
            {
                _busConfigurator.AddConsumer<T>(c =>
                {
                    c.UseInMemoryOutbox();
                    c.UseMessageRetry(configureRetries.Invoke);
                });
            }

            return this;
        }
    }
}