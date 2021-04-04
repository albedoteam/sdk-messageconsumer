namespace AlbedoTeam.Sdk.MessageConsumer.Configuration
{
    using System;
    using Abstractions;

    internal class BrokerConfigurator : IBrokerConfigurator
    {
        public IMessageBrokerOptions Options { get; private set; }

        public IBrokerConfigurator SetBrokerOptions(Action<IMessageBrokerOptions> configureBrokerOptions)
        {
            IMessageBrokerOptions brokerOptions = new MessageBrokerOptions();
            configureBrokerOptions.Invoke(brokerOptions);

            if (string.IsNullOrWhiteSpace(brokerOptions.HostOptions.Host))
                throw new InvalidOperationException("Can not start the service without a valid Message Broker Host");

            Options = brokerOptions;

            return this;
        }
    }
}