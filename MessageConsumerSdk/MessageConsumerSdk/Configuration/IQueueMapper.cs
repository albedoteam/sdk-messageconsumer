namespace MessageConsumerSdk.Configuration
{
    public interface IQueueMapper
    {
        void Map<T>() where T : class;
    }
}