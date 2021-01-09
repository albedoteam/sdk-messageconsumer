using AlbedoTeam.Sdk.MessageConsumer.EventStore.Models;
using MassTransit.Audit;

namespace AlbedoTeam.Sdk.MessageConsumer.EventStore.Mappers
{
    public interface IMessageMapper
    {
        EventAuditMetadata MapMessageAuditMetadataToModel(MessageAuditMetadata message);
    }
}