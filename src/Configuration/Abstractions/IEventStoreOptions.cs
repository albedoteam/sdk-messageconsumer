namespace AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions
{
    public interface IEventStoreOptions
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}