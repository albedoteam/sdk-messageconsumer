using System.Collections.Generic;
using MessageConsumerSdk.Configuration.Abstractions;

namespace MessageConsumerSdk.Configuration
{
    public class ConsumerOptions : IConsumerOptions
    {
        public string BrokerHost { get; set; }
        public Dictionary<string, string> DestinationQueues { get; set; }
    }
}