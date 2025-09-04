using AutoMapper;
using Million.Properties.Application.DTOs;
using Million.Properties.Domain.Entities;
using Million.Properties.Domain.Entities.Request;

namespace Million.Properties.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreatePropertyRequest, Property>()
            .ForMember(dest => dest.IdProperty, opt => opt.MapFrom(src => src.IdProperty))
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.InternalCode, opt => opt.MapFrom(_ => Guid.NewGuid().ToString()));

        CreateMap<CreatePropertyDto, Property>()
            .ForMember(dest => dest.IdProperty, opt => opt.MapFrom(src => src._id))
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.InternalCode, opt => opt.MapFrom(_ => Guid.NewGuid().ToString()));

        CreateMap<Property, PropertyDto>()
            .ForMember(d => d.IdProperty, o => o.MapFrom(s => s.IdProperty))
            .ForMember(d => d.IdOwner, o => o.MapFrom(s => s.IdOwner ?? string.Empty))
            .ForMember(d => d.File, o => o.Ignore()) // se llena en el handler/repositorio
            .ForMember(d => d.InternalCode, o => o.MapFrom(s => s.InternalCode));

        CreateMap<PropertyDto, Property>()
            .ForMember(d => d.InternalCode, o => o.MapFrom(s => s.InternalCode));

        CreateMap<Property, CreatePropertyDto>()
            .ForMember(d => d._id, o => o.MapFrom(s => s.IdProperty))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
            .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
            .ForMember(d => d.File, o => o.Ignore()) // igual, se llena aparte
            .ForMember(d => d.InternalCode, o => o.Ignore());
    }
}
