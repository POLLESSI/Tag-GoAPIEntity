using AutoMapper;
using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mappages pour Activity et DTOs
            CreateMap<Activity, ActivityDto>();

            CreateMap<ActivityDto, Activity>();
            CreateMap<ActivityCreationDto, Activity>();
            CreateMap<ActivityEditionDto, Activity>();

            CreateMap<User, UserDto>();

            CreateMap<UserDto, User>();
            CreateMap<UserCreationDto, User>();
        }
    }
}
