namespace AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions
{
    public interface IDestinationQueueMapper
    {
        IDestinationQueueMapper Map<T>() where T : class;
    }
}