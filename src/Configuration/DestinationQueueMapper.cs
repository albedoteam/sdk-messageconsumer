namespace AlbedoTeam.Sdk.MessageConsumer.Configuration
{
    using System;
    using Abstractions;
    using MassTransit;
    using MassTransit.Definition;

    public class DestinationQueueMapper : IDestinationQueueMapper
    {
        public IDestinationQueueMapper Map<T>() where T : class
        {
            var interfaceQueueName = typeof(T).Name;
            var queueName = KebabCaseEndpointNameFormatter.Instance.SanitizeName(interfaceQueueName);

            EndpointConvention.Map<T>(new Uri($"queue:{queueName}"));

            return this;
        }
    }
}