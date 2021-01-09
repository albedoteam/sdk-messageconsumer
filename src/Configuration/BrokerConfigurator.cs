using System;
using AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions;

namespace AlbedoTeam.Sdk.MessageConsumer.Configuration
{
    internal class BrokerConfigurator : IBrokerConfigurator
    {
        public IMessageBrokerOptions Options { get; private set; }
        public IEventStoreOptions EventStoreOptions { get; private set; }

        public IBrokerConfigurator SetBrokerOptions(Action<IMessageBrokerOptions> configureBrokerOptions)
        {
            IMessageBrokerOptions brokerOptions = new MessageBrokerOptions();
            configureBrokerOptions.Invoke(brokerOptions);

            if (string.IsNullOrWhiteSpace(brokerOptions.Host))
                throw new InvalidOperationException("Can not start the service without a valid Message Broker Host");

            Options = brokerOptions;

            return this;
        }

        public IBrokerConfigurator AddEventStore(Action<IEventStoreOptions> configureEventStore)
        {
            IEventStoreOptions eventStoreOptions = new EventStoreOptions();
            configureEventStore.Invoke(eventStoreOptions);

            if (string.IsNullOrWhiteSpace(eventStoreOptions.ConnectionString))
                throw new InvalidOperationException(
                    "Can not start the event store without a valid Database ConnectionString");

            if (string.IsNullOrWhiteSpace(eventStoreOptions.DatabaseName))
                throw new InvalidOperationException("Can not start the event store without a valid Database Name");

            EventStoreOptions = eventStoreOptions;

            return this;
        }
    }
}