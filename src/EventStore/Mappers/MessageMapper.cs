using System.Collections.Generic;
using System.Linq;
using AlbedoTeam.Sdk.EventStore.Contracts;
using AlbedoTeam.Sdk.MessageConsumer.EventStore.Models;
using AutoMapper;
using MassTransit.Audit;

namespace AlbedoTeam.Sdk.MessageConsumer.EventStore.Mappers
{
    public class MessageMapper : IMessageMapper
    {
        private readonly IMapper _mapper;

        public MessageMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MessageAuditMetadata, EventAuditMetadata>().ReverseMap();
                cfg.CreateMap<EventOcurred, EventRedeliveryResponse>().ReverseMap();
                cfg.CreateMap<EventAuditMetadata, EventRedeliveryMetadataResponse>().ReverseMap();
            });

            _mapper = config.CreateMapper();
        }

        public EventAuditMetadata MapMessageAuditMetadataToModel(MessageAuditMetadata message)
        {
            return _mapper.Map<MessageAuditMetadata, EventAuditMetadata>(message);
        }

        public List<EventRedeliveryResponse> MapModelToResponse(IReadOnlyList<EventOcurred> toList)
        {
            return _mapper.Map<List<EventOcurred>, List<EventRedeliveryResponse>>(toList.ToList());
        }
    }
}