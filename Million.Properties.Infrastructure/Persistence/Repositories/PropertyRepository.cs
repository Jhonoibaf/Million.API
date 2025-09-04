using Million.Properties.Application.Contracts.Persistence;
using Million.Properties.Domain.Entities;
using MongoDB.Driver;


namespace Million.Properties.Infrastructure.Persistence.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly MongoDbContext _collection;

    public PropertyRepository(MongoDbContext context)
    {
        _collection = context;
    }

    public async Task<Property?> GetByIdAsync(string id) =>
        await _collection.Properties.Find(x => x.IdProperty == id).FirstOrDefaultAsync();

    public async Task<IEnumerable<Property>> GetAllAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice)
    {
        var filter = Builders<Property>.Filter.Empty;

        if (!string.IsNullOrWhiteSpace(name))
            filter &= Builders<Property>.Filter.Regex(x => x.Name, new MongoDB.Bson.BsonRegularExpression(name, "i"));

        if (!string.IsNullOrWhiteSpace(address))
            filter &= Builders<Property>.Filter.Regex(x => x.Address, new MongoDB.Bson.BsonRegularExpression(address, "i"));

        if (minPrice.HasValue)
            filter &= Builders<Property>.Filter.Gte(x => x.Price, minPrice.Value);

        if (maxPrice.HasValue)
            filter &= Builders<Property>.Filter.Lte(x => x.Price, maxPrice.Value);

        return await _collection.Properties.Find(filter).ToListAsync();
    }

    public async Task AddAsync(Property property) =>
        await _collection.Properties.InsertOneAsync(property);

    public async Task UpdateAsync(Property property) =>
        await _collection.Properties.ReplaceOneAsync(x => x.IdProperty == property.IdProperty, property);

    public async Task DeleteAsync(string id) =>
        await _collection.Properties.DeleteOneAsync(x => x.IdProperty == id);
}
