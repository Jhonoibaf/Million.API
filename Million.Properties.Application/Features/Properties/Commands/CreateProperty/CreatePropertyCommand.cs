using MediatR;
using Million.Properties.Application.DTOs;

namespace Million.Properties.Application.Features.Properties.Commands.CreateProperty;

public class CreatePropertyCommand(string name, string address, decimal price)
: IRequest<CreatePropertyDto>
{
    public string Name { get; set; } = name;
    public string Address { get; set; } = address;
    public decimal Price { get; set; } = price;
}

