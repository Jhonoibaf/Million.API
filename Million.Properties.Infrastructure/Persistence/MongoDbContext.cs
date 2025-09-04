
using Million.Properties.Domain.Entities;
using MongoDB.Driver;

namespace Million.Properties.Infrastructure.Persistence;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    public MongoDbContext(IMongoDatabase db) => _database = db;

    public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("owners");
    public IMongoCollection<Property> Properties => _database.GetCollection<Property>("properties");
    public IMongoCollection<PropertyImage> PropertyImages => _database.GetCollection<PropertyImage>("propertyImages");
    public IMongoCollection<PropertyTrace> PropertyTraces => _database.GetCollection<PropertyTrace>("propertyTraces");

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
}
