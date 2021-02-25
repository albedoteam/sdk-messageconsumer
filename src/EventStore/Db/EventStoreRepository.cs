using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AlbedoTeam.Sdk.DataLayerAccess;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AlbedoTeam.Sdk.MessageConsumer.EventStore.Db
{
    public class EventStoreRepository : BaseRepository<EventOcurred>, IEventStoreRepository
    {
        public EventStoreRepository(IDbContext<EventOcurred> context) : base(context)
        {
        }
    }
}