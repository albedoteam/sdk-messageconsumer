using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Db;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Models;
using Microsoft.Extensions.Logging;

namespace AlbedoTeam.Sdk.MessageConsumer.EventStore.Services
{
    public class EventStoreService : IEventStoreService
    {
        private readonly ILogger<EventStoreService> _logger;
        private readonly IEventStoreRepository _eventStore;

        public EventStoreService(IEventStoreRepository eventStore, ILogger<EventStoreService> logger)
        {
            _eventStore = eventStore;
            _logger = logger;
        }

        public async Task Redelivery(DateTime since)
        {
            await RedeliveryMessages(since);
        }

        public async Task Redelivery(DateTime since, DateTime until)
        {
            await RedeliveryMessages(since, until);
        }

        private async Task RedeliveryMessages(DateTime since, DateTime? until = null)
        {
            Expression<Func<EventOcurred, bool>> filterDefinition = e => e.Metadata.SentTime >= since;

            const int pageSize = 5;
            var page = 1;
            var totalPages = 0;

            do
            {
                var (i, readOnlyList) = await _eventStore.QueryByPage(
                    page, 
                    pageSize, 
                    filterDefinition, 
                    e => e.Metadata.SentTime);

                WriteResults(page, readOnlyList);
                page += 1;
                totalPages = i;
            } while (page <= totalPages);
        }

        private void WriteResults(int page, IEnumerable<EventOcurred> messages)
        {
            _logger.LogInformation($"Page: {page}");

            foreach (var message in messages)
            {
                _logger.LogInformation($"{message.EventType} - {message.Metadata.MessageId}: {message.Message}");
            }
        }
    }
}