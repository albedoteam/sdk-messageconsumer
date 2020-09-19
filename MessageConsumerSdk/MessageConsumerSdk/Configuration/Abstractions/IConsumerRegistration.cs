using System;
using GreenPipes.Configurators;
using MassTransit;

namespace MessageConsumerSdk.Configuration.Abstractions
{
    public interface IConsumerRegistration
    {
        void Add<T>(Action<IRetryConfigurator> configureRetries = null) where T : class, IConsumer;
    }
}