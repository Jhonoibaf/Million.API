using AutoMapper;
using Million.Properties.Application.DTOs;
using Million.Properties.Domain.Entities;

namespace Million.Properties.Application.Mappings;

public class MappingProfile : Profile
{
    
        public MappingProfile()
        {

        CreateMap<PropertyDto, Property>()
            .ForMember(dest => dest.IdProperty, opt => opt.MapFrom(src => src.IdProperty));
                
            CreateMap<CreatePropertyDto, Property>()
                .ForMember(dest => dest.IdProperty, opt => opt.MapFrom(src => src._id))
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore());

        CreateMap<Property, PropertyDto>()
            .ForMember(d => d.IdProperty, o => o.MapFrom(s => s.IdProperty))
            .ForMember(d => d.IdOwner, o => o.MapFrom(s => s.IdOwner ?? string.Empty))
            .ForMember(d => d.File, o => o.Ignore());

    }
}
