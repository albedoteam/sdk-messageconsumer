using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AlbedoTeam.Sdk.EventStore.Contracts;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Contracts.Requests;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Db;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Mappers;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Models;
using MassTransit;
using MongoDB.Driver;

namespace AlbedoTeam.Sdk.MessageConsumer.EventStore.Consumers
{
    public class EventRedeliveryRequestConsumer : IConsumer<EventRedeliveryRequest>
    {
        private readonly IEventStoreRepository _eventStore;
        private readonly IMessageMapper _mapper;

        public EventRedeliveryRequestConsumer(IEventStoreRepository eventStore, IMessageMapper mapper)
        {
            _eventStore = eventStore;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<EventRedeliveryRequest> context)
        {
            var filterBy = Builders<EventOcurred>.Filter.And(
                Builders<EventOcurred>.Filter.Gte(e => e.Metadata.SentTime, context.Message.Since));

            if (!string.IsNullOrWhiteSpace(context.Message.EventType))
            {
                filterBy &= Builders<EventOcurred>.Filter.Eq(e => e.EventType, context.Message.EventType);
            }

            var orderBy = Builders<EventOcurred>.Sort.Ascending(e => e.Metadata.SentTime);
                
            var (totalPages, messages) = await _eventStore.QueryByPage(
                context.Message.Page,
                context.Message.PageSize,
                filterBy,
                orderBy);

            await context.RespondAsync<PagedRedeliveryReponse>(new
            {
                TotalPages = totalPages,
                Messages = _mapper.MapModelToResponse(messages)
            });
        }
    }
}