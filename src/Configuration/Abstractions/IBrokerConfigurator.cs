using System;

namespace AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions
{
    public interface IBrokerConfigurator
    {
        IMessageBrokerOptions Options { get; }
        IEventStoreOptions EventStoreOptions { get; }

        IBrokerConfigurator SetBrokerOptions(Action<IMessageBrokerOptions> configureBrokerOptions);
        IBrokerConfigurator AddEventStore(Action<IEventStoreOptions> configureEventStore);
    }
}