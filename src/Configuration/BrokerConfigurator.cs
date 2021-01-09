using System;
using AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions;

namespace AlbedoTeam.Sdk.MessageConsumer.Configuration
{
    internal class BrokerConfigurator : IBrokerConfigurator
    {
        public IMessageBrokerOptions Options { get; private set; }
        public bool UseEventStore { get; private set; }

        public IBrokerConfigurator SetBrokerOptions(Action<IMessageBrokerOptions> configureBrokerOptions)
        {
            IMessageBrokerOptions brokerOptions = new MessageBrokerOptions();
            configureBrokerOptions.Invoke(brokerOptions);

            if (string.IsNullOrWhiteSpace(brokerOptions.Host))
                throw new InvalidOperationException("Can not start the service without a valid Message Broker Host");

            Options = brokerOptions;

            return this;
        }

        public IBrokerConfigurator AddEventStore()
        {
            UseEventStore = true;
            return this;
        }
    }
}