﻿using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using AlbedoTeam.Sdk.DataLayerAccess.Attributes;

namespace AlbedoTeam.Sdk.MessageConsumer.EventStore.Models
{
    [BsonCollection("eventStore")]
    public class EventOcurred : Document
    {
        public dynamic Message { get; set; }
        public EventAuditMetadata Metadata { get; set; }
    }
}