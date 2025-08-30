
using Microsoft.Extensions.Configuration;
using Million.Properties.Infrastructure.Persintence.Models;
using MongoDB.Driver;

namespace Million.Properties.Infrastructure.Persintence
{
    internal class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(IMongoDatabase db) => _database = db;

        public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("Owners");
        public IMongoCollection<Property> Properties => _database.GetCollection<Property>("Properties");
        public IMongoCollection<PropertyImage> PropertyImages => _database.GetCollection<PropertyImage>("PropertyImages");
        public IMongoCollection<PropertyTrace> PropertyTraces => _database.GetCollection<PropertyTrace>("PropertyTraces");
    }
}
