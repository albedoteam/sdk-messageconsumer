namespace AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions
{
    public interface IDestinationQueueMapper
    {
        void Map<T>() where T : class;
    }
}