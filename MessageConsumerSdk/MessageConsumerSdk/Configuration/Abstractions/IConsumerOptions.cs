using System.Collections.Generic;

namespace MessageConsumerSdk.Configuration.Abstractions
{
    public interface IConsumerOptions
    {
        public string BrokerHost { get; set; }
        public Dictionary<string, string> DestinationQueues { get; set; }
    }
}