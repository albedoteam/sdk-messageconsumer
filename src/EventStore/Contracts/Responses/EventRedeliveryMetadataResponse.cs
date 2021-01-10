using System;
using System.Collections.Generic;

namespace AlbedoTeam.Sdk.EventStore.Contracts
{
    public interface EventRedeliveryMetadataResponse
    {
        Guid? MessageId { get; set; }
        Guid? ConversationId { get; set; }
        Guid? CorrelationId { get; set; }
        Guid? InitiatorId { get; set; }
        Guid? RequestId { get; set; }
        DateTime? SentTime { get; set; }
        string SourceAddress { get; set; }
        string DestinationAddress { get; set; }
        string InputAddress { get; set; }
        string ResponseAddress { get; set; }
        string FaultAddress { get; set; }
        string ContextType { get; set; }
        Dictionary<string, string> Headers { get; set; }
        Dictionary<string, string> Custom { get; set; }
    }
}