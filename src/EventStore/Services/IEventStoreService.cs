using System;
using System.Threading.Tasks;

namespace AlbedoTeam.Sdk.MessageConsumer.EventStore.Services
{
    public interface IEventStoreService
    {
        Task Redelivery(DateTime since);
        Task Redelivery(DateTime since, DateTime until);
    }
}