using System.Collections.Generic;
using AlbedoTeam.Sdk.EventStore.Contracts;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Models;
using MassTransit.Audit;

namespace AlbedoTeam.Sdk.MessageConsumer.EventStore.Mappers
{
    public interface IMessageMapper
    {
        EventAuditMetadata MapMessageAuditMetadataToModel(MessageAuditMetadata message);
        List<EventRedeliveryResponse> MapModelToResponse(IReadOnlyList<EventOcurred> toList);
    }
}