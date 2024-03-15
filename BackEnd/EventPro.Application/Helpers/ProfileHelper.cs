using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities;
using EventPro.Domain.ContextEvent.Entities.Identity;

namespace EventPro.Application.Helpers;

public class ProfileHelper : Profile
{
    public ProfileHelper()
    {
        CreateMap<Event, EventDto>().ReverseMap();
        CreateMap<Event, CreateEventCommand>().ReverseMap();
        CreateMap<Event, UpdateEventCommand>().ReverseMap();
        CreateMap<Lot, LotDto>().ReverseMap();
        CreateMap<LotDto, CreateLotCommand>().ReverseMap();
        CreateMap<LotDto, UpdateLotCommand>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserLoginDto>().ReverseMap();
    }
}