using System.Collections.Generic;

namespace AlbedoTeam.Sdk.EventStore.Contracts
{
    public interface PagedRedeliveryReponse
    {
        int TotalPages { get; set; }
        List<EventRedeliveryResponse> Messages { get; set; }
    }
}