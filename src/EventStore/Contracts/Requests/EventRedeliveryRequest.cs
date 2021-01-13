using System;

namespace AlbedoTeam.Sdk.MessageConsumer.EventStore.Contracts.Requests
{
    public interface EventRedeliveryRequest
    {
        int Page { get; set; }
        int PageSize { get; set; }
        DateTime Since { get; set; }
        DateTime? Until { get; set; }
        string EventType { get; set; }
    }
}