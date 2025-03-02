
using AutoMapper;
using MyApi.Application.DTOs.ActivityDTOs;
using MyApi.Application.DTOs.UserDTOs;
using MyApi.Domain.Entities;

namespace MyApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mappages pour Activity et DTOs
            CreateMap<ActivityEntity, ActivityDto>();

            CreateMap<ActivityDto, ActivityEntity>();
            CreateMap<ActivityCreationDto, ActivityEntity>();
            CreateMap<ActivityEditionDto, ActivityEntity>();

            CreateMap<UserEntity, UserDto>();

            CreateMap<UserDto, UserEntity>();
            CreateMap<UserCreationDto, UserEntity>();
        }
    }
}
