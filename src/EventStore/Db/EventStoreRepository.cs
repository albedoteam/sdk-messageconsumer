using AlbedoTeam.Sdk.DataLayerAccess;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Models;

namespace AlbedoTeam.Sdk.MessageConsumer.EventStore.Db
{
    public class EventStoreRepository : BaseRepository<EventOcurred>, IEventStoreRepository
    {
        public EventStoreRepository(IDbContext<EventOcurred> context) : base(context)
        {
        }
    }
}