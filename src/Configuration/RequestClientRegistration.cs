using AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions;
using MassTransit.ExtensionsDependencyInjectionIntegration;

namespace AlbedoTeam.Sdk.MessageConsumer.Configuration
{
    public class RequestClientRegistration : IRequestClientRegistration
    {
        private readonly IServiceCollectionBusConfigurator _busConfigurator;

        public RequestClientRegistration(IServiceCollectionBusConfigurator busConfigurator)
        {
            _busConfigurator = busConfigurator;
        }

        public void AddRequestClient<T>() where T : class
        {
            _busConfigurator.AddRequestClient<T>();
        }
    }
}