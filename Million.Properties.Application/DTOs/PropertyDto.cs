﻿
namespace Million.Properties.Application.DTOs;

public class PropertyDto
{
    public string IdProperty { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string InternalCode { get; set; } 
    public int Year { get; set; }
    public string IdOwner { get; set; } = string.Empty;
    public string File { get; set; } = string.Empty;
}
