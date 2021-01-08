using System;
using AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions;
using MassTransit;

namespace AlbedoTeam.Sdk.MessageConsumer.Configuration
{
    public class DestinationQueueMapper : IDestinationQueueMapper
    {
        private readonly IMessageBrokerOptions _options;

        public DestinationQueueMapper(IMessageBrokerOptions options)
        {
            _options = options;
        }

        public void Map<T>() where T : class
        {
            var interfaceQueueName = typeof(T).Name;

            if (!_options.DestinationQueues.TryGetValue(interfaceQueueName, out var queue))
                throw new InvalidOperationException("Can not start the service without a valid Message Queue Name");

            if (string.IsNullOrWhiteSpace(queue))
                throw new InvalidOperationException("Can not start the service without a valid Message Queue Name");

            EndpointConvention.Map<T>(new Uri($"queue:{queue}"));
        }
    }
}