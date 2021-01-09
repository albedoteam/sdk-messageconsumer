using AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions;

namespace AlbedoTeam.Sdk.MessageConsumer.Configuration
{
    internal class EventStoreOptions : IEventStoreOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}