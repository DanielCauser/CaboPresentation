using AutoMapper;
using CaboAPI.DTOs;
using CaboAPI.Entities;
using Microsoft.ApplicationInsights;

namespace CaboAPI.MapperConfig
{
    public class DtoMappingProfile : Profile {
        public DtoMappingProfile() {
            // Add as many of these lines as you need to map your objects
            CreateMap<TodoItem, TodoItemDto>().ReverseMap();
            CreateMap<TodoCabo, TodoCabo2Dto>().ReverseMap();
            CreateMap<TodoCabo, TodoCaboDto>(MemberList.None)
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.NameActivity))
                .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.DateStarted))
                .ReverseMap()
                .ForMember(dst => dst.NameActivity, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.DateStarted, opt => opt.MapFrom(src => src.Date));
        }
    }
}