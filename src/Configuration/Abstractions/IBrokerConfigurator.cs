using System;

namespace AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions
{
    public interface IBrokerConfigurator
    {
        IMessageBrokerOptions Options { get; }
        bool UseEventStore { get; }

        IBrokerConfigurator SetBrokerOptions(Action<IMessageBrokerOptions> configureBrokerOptions);
        IBrokerConfigurator AddEventStore();
    }
}