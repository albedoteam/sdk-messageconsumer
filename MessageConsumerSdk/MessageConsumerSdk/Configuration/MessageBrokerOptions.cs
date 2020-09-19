using System.Collections.Generic;
using MessageConsumerSdk.Configuration.Abstractions;

namespace MessageConsumerSdk.Configuration
{
    public class MessageBrokerOptions : IMessageBrokerOptions
    {
        public string Host { get; set; }
        public Dictionary<string, string> DestinationQueues { get; set; }
    }
}