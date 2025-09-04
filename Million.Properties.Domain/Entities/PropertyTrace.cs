using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Million.Properties.Domain.Entities;

public class PropertyTrace
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdPropertyTrace { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.ObjectId)]
    public int IdProperty { get; set; } 
    public DateTime DateSale { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public decimal Tax { get; set; }
}
