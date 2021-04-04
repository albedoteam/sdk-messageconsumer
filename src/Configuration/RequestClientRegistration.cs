namespace AlbedoTeam.Sdk.MessageConsumer.Configuration
{
    using Abstractions;
    using MassTransit.ExtensionsDependencyInjectionIntegration;

    public class RequestClientRegistration : IRequestClientRegistration
    {
        private readonly IServiceCollectionBusConfigurator _busConfigurator;

        public RequestClientRegistration(IServiceCollectionBusConfigurator busConfigurator)
        {
            _busConfigurator = busConfigurator;
        }

        public IRequestClientRegistration Add<T>() where T : class
        {
            _busConfigurator.AddRequestClient<T>();
            return this;
        }
    }
}