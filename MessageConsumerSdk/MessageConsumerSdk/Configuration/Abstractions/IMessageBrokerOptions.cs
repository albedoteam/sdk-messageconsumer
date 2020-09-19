using System.Collections.Generic;

namespace MessageConsumerSdk.Configuration.Abstractions
{
    public interface IMessageBrokerOptions
    {
        public string Host { get; set; }
        public Dictionary<string, string> DestinationQueues { get; set; }
    }
}