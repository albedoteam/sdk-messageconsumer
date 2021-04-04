namespace AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions
{
    using System;

    public interface IBrokerConfigurator
    {
        IMessageBrokerOptions Options { get; }

        IBrokerConfigurator SetBrokerOptions(Action<IMessageBrokerOptions> configureBrokerOptions);
    }
}