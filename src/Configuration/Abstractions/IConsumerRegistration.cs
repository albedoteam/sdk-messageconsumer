using System;
using GreenPipes.Configurators;
using MassTransit;

namespace AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions
{
    public interface IConsumerRegistration
    {
        void Add<T>(Action<IRetryConfigurator> configureRetries = null) where T : class, IConsumer;
    }
}