namespace AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions
{
    public interface IRequestClientRegistration
    {
        void AddRequestClient<T>() where T : class;
    }
}