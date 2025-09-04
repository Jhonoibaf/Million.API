using MediatR;
using Million.Properties.Application.DTOs;

namespace Million.Properties.Application.Features.Properties.Queries.GetPropertyById;

public class GetPropertyByIdQuery(string id) : IRequest<PropertyDto?>
{
    public string Id { get; set; } = id;
}   