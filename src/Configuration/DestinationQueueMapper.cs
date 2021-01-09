using System;
using AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions;
using MassTransit;
using MassTransit.Definition;

namespace AlbedoTeam.Sdk.MessageConsumer.Configuration
{
    public class DestinationQueueMapper : IDestinationQueueMapper
    {
        public void Map<T>() where T : class
        {
            var interfaceQueueName = typeof(T).Name;
            var queueName = KebabCaseEndpointNameFormatter.Instance.SanitizeName(interfaceQueueName);

            EndpointConvention.Map<T>(new Uri($"queue:{queueName}"));
        }
    }
}