using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Db;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Mappers;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Models;
using MassTransit.Audit;

namespace AlbedoTeam.Sdk.MessageConsumer.Configuration
{
    public class MongoMessageAuditStore : IMessageAuditStore
    {
        private readonly IEventStoreRepository _eventStore;
        private readonly IMessageMapper _mapper;

        public MongoMessageAuditStore(IEventStoreRepository eventStore, IMessageMapper mapper)
        {
            _eventStore = eventStore;
            _mapper = mapper;
        }

        public async Task StoreMessage<T>(T message, MessageAuditMetadata metadata) where T : class
        {
            if (metadata.ContextType == "Publish")
            {
                const string pattern = @"(:[A-Z])\w+";
                var rg = new Regex(pattern);
                var match = rg.Match(metadata.DestinationAddress);

                await _eventStore.InsertOne(new EventOcurred
                {
                    Message = message,
                    Metadata = _mapper.MapMessageAuditMetadataToModel(metadata),
                    EventType = match.Success ? match.Value.Replace(":", "") : match.Value
                });
            }

            await Task.CompletedTask;
        }
    }
}