using AutoMapper;
using MessageService.DTO;
using MessageService.Models;

namespace MessageService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Message, MessageViewModel>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.FromUser, opt => opt.MapFrom(src => src.FromUser))
                .ForMember(dest => dest.ToUser, opt => opt.MapFrom(src => src.ToUser))
                .ReverseMap();
        }
    }
}
