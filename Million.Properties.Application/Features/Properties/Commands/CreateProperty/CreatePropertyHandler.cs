using AutoMapper;
using MediatR;
using Million.Properties.Application.Contracts.Persistence;
using Million.Properties.Application.DTOs;
using Million.Properties.Domain.Entities;

namespace Million.Properties.Application.Features.Properties.Commands.CreateProperty;

public class CreatePropertyHandler(IPropertyRepository repository, IMapper mapper)
    : IRequestHandler<CreatePropertyCommand, CreatePropertyDto>
{
    private readonly IPropertyRepository _propertyRepository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<CreatePropertyDto> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new CreatePropertyDto
            {
                Name = request.Name,
                Address = request.Address,
                Price = request.Price,
            };

            var property = _mapper.Map<Property>(entity);
            await _propertyRepository.AddAsync(property);

            return new CreatePropertyDto
            {
                Name = entity.Name,
                Address = entity.Address,
                Price = entity.Price
            };
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the property.", ex);
        }
    }
}
