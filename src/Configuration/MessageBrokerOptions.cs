using System.Collections.Generic;
using AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions;

namespace AlbedoTeam.Sdk.MessageConsumer.Configuration
{
    public class MessageBrokerOptions : IMessageBrokerOptions
    {
        public string Host { get; set; }
        public Dictionary<string, string> DestinationQueues { get; set; }
    }
}