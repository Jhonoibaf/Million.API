using AutoMapper;
using MediatR;
using Million.Properties.Application.Contracts.Persistence;
using Million.Properties.Application.DTOs;

namespace Million.Properties.Application.Features.Properties.Queries.GetAllProperties;

public class GetAllPropertiesHandler(
        IPropertyRepository repository,
        IPropertyImageRepository imageRepo,
        IMapper mapper
    ) : IRequestHandler<GetAllPropertiesQuery, IEnumerable<PropertyDto>>
{
    private readonly IPropertyRepository _propertyRepository = repository;
    private readonly IPropertyImageRepository _imageRepo = imageRepo;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<PropertyDto>> Handle(GetAllPropertiesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var props = await _propertyRepository.GetAllAsync(
                query.Request.Name,
                query.Request.Address,
                query.Request.MinPrice,
                query.Request.MaxPrice
            );

            var ids = props.Select(p => p.IdProperty).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            var imagesByProp = await _imageRepo.GetFirstByPropertyIdsAsync(ids);

            var dtos = _mapper.Map<IEnumerable<PropertyDto>>(props).ToList();

            foreach (var dto in dtos)
            {
                if (imagesByProp.TryGetValue(dto.IdProperty, out var file) && !string.IsNullOrWhiteSpace(file))
                    dto.File = file;
            }

            return dtos;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while fetching properties.", ex);
        }
    }
}
