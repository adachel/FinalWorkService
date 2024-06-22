using static System.Runtime.InteropServices.JavaScript.JSType;
using UserService.DTO;
using AutoMapper;
using UserService.Models;

namespace UserService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();
        }
    }
}
