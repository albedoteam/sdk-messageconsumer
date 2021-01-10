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
        private readonly IMongoCollection<EventOcurred> _collection;

        public EventStoreRepository(IDbContext<EventOcurred> context) : base(context)
        {
            _collection = context.GetCollection();
        }

        public new async Task DeleteById(string id)
        {
            var objectId = new ObjectId(id);
            await _collection.DeleteOneAsync(t => t.Id == objectId);
        }

        // public async Task<(int totalPages, IReadOnlyList<EventOcurred> readOnlyList)> QueryByPage(
        //     int page, 
        //     int pageSize,
        //     Expression<Func<EventOcurred, bool>> filterExpression)
        // {
        //     var countFacet = AggregateFacet.Create("count",
        //         PipelineDefinition<EventOcurred, AggregateCountResult>.Create(new[]
        //         {
        //             PipelineStageDefinitionBuilder.Count<EventOcurred>()
        //         }));
        //     
        //     var dataFacet = AggregateFacet.Create("data",
        //         PipelineDefinition<EventOcurred, EventOcurred>.Create(new[]
        //         {
        //             PipelineStageDefinitionBuilder.Sort(Builders<EventOcurred>.Sort.Ascending(x => x.Metadata.SentTime)),
        //             PipelineStageDefinitionBuilder.Skip<EventOcurred>((page - 1) * pageSize),
        //             PipelineStageDefinitionBuilder.Limit<EventOcurred>(pageSize),
        //         }));
        //
        //     var aggregation = await _collection.Aggregate()
        //         .Match(filterExpression)
        //         .Facet(countFacet, dataFacet)
        //         .ToListAsync();
        //     
        //     var count = aggregation.First()
        //         .Facets.First(x => x.Name == "count")
        //         .Output<AggregateCountResult>()
        //         .First()
        //         .Count;
        //
        //     var rest = (count % pageSize);
        //     var totalPages = (int)count / pageSize;
        //     if (rest > 0) totalPages += 1;
        //     
        //     var data = aggregation.First()
        //         .Facets.First(x => x.Name == "data")
        //         .Output<EventOcurred>();
        //     
        //     return (totalPages, data);
        // }
    }
}