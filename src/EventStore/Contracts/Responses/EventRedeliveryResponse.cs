namespace AlbedoTeam.Sdk.EventStore.Contracts
{
    public interface EventRedeliveryResponse
    {
            string EventType { get; set; }
            dynamic Message { get; set; }
            EventRedeliveryMetadataResponse Metadata { get; set; }
    }
}