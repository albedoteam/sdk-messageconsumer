using System;
using System.Linq;
using MassTransit;

namespace MessageConsumerSdk.Configuration
{
    public class QueueMapper: IQueueMapper
    {
        private readonly IConsumerOptions _options;

        public QueueMapper(IConsumerOptions options)
        {
            _options = options;
        }

        public void Map<T>() where T : class
        {
            var queueName = typeof(T).Name;

            var queue = _options.Queues.FirstOrDefault(q => q.Equals(queueName));
            if (string.IsNullOrWhiteSpace(queue))
                throw new InvalidOperationException("Can not start the service without a valid Message Queue Name");

            EndpointConvention.Map<T>(new Uri($"queue:{queue}"));
        }
    }
}