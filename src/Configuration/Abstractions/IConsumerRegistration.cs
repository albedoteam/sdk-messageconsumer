namespace AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions
{
    using System;
    using GreenPipes.Configurators;
    using MassTransit;

    public interface IConsumerRegistration
    {
        IConsumerRegistration Add<T>(Action<IRetryConfigurator> configureRetries = null) where T : class, IConsumer;
    }
}