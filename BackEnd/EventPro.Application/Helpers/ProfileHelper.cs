using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;

namespace EventPro.Application.Helpers;

public class ProfileHelper : Profile
{
    public ProfileHelper()
    {
        CreateMap<Event, EventDto>().ReverseMap();
        CreateMap<Lot, LotDto>().ReverseMap();
        CreateMap<Speaker, SpeakerDto>().ReverseMap();
        CreateMap<SocialNetwork, SocialNetworkDto>().ReverseMap();
        CreateMap<LotDto, CreateLotCommand>().ReverseMap();
        CreateMap<LotDto, UpdateLotCommand>().ReverseMap();
    }
}