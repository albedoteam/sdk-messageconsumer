using AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions;

namespace AlbedoTeam.Sdk.MessageConsumer.Configuration
{
    internal class MessageBrokerOptions : IMessageBrokerOptions
    {
        public string Host { get; set; }
    }
}