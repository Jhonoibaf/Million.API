using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Million.Properties.Infrastructure.Persistence.Models
{
    internal class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdProperty { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;    
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string InternalCode { get; set; } = string.Empty;
        public int Year { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string IdOwner { get; set; } = string.Empty;
    }
}
