namespace MessageConsumerSdk.Configuration
{
    public interface IDestinationQueueMapper
    {
        void Map<T>() where T : class;
    }
}