using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AlbedoTeam.Sdk.MessageConsumer.EventStore.Models
{
    public class EventAuditMetadata
    {
        [BsonRepresentation(BsonType.String)]
        public Guid? MessageId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid? ConversationId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid? CorrelationId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid? InitiatorId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid? RequestId { get; set; }

        public DateTime? SentTime { get; set; }

        public string SourceAddress { get; set; }

        public string DestinationAddress { get; set; }

        public string InputAddress { get; set; }

        public string ResponseAddress { get; set; }

        public string FaultAddress { get; set; }

        public string ContextType { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public Dictionary<string, string> Custom { get; set; }
    }
}