using Million.Properties.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Million.Properties.Domain.Entities;

public class Property: BaseEntityModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdProperty { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public string InternalCode { get; set; } = Guid.NewGuid().ToString();

    public int Year { get; set; }
    public string? File { get; set; } 

    [BsonRepresentation(BsonType.ObjectId)]
    public string? IdOwner { get; set; } 
}
