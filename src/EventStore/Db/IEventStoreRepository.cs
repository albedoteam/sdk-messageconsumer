using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Models;

namespace AlbedoTeam.Sdk.MessageConsumer.EventStore.Db
{
    public interface IEventStoreRepository : IBaseRepository<EventOcurred>
    {
    }
}