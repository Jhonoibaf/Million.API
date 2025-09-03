using AutoMapper;
using MediatR;
using Million.Properties.Application.Contracts.Persistence;
using Million.Properties.Application.DTOs;

namespace Million.Properties.Application.Features.Properties.Queries.GetPropertyById;

public class GetPropertyByIdHandler(
    IPropertyRepository propertyRepository,
    IPropertyImageRepository imageRepository,
    IMapper mapper)
    : IRequestHandler<GetPropertyByIdQuery, PropertyDto?>
{
    private readonly IPropertyRepository _propertyRepository = propertyRepository;
    private readonly IPropertyImageRepository _imageRepository = imageRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PropertyDto?> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var property = await _propertyRepository.GetByIdAsync(request.Id);
            if (property == null) return null;

            var dto = _mapper.Map<PropertyDto>(property);

            var imageDict = await _imageRepository.GetFirstByPropertyIdsAsync(new[] { property.IdProperty });
            if (imageDict.TryGetValue(property.IdProperty, out var file) && !string.IsNullOrEmpty(file))
            {
                dto.File = file;
            }

            return dto;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while getting Property", ex);
        }
    }
}
