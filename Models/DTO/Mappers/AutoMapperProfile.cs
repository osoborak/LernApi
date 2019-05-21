using AutoMapper;

namespace LernApi.Models.DTO.Mappers

{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User,UserInfo>();
            CreateMap<UserInfo,User>();
        }
    }
}


