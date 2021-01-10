using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Models;

namespace AlbedoTeam.Sdk.MessageConsumer.EventStore.Db
{
    public interface IEventStoreRepository : IBaseRepository<EventOcurred>
    {
        // Task<(int totalPages, IReadOnlyList<EventOcurred> readOnlyList)> QueryByPage(
        //     int page,
        //     int pageSize,
        //     Expression<Func<EventOcurred, bool>> filterExpression);
    }
}