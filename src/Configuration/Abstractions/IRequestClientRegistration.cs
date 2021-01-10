namespace AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions
{
    public interface IRequestClientRegistration
    {
        IRequestClientRegistration Add<T>() where T : class;
    }
}