using MongoDB.Driver;
using Million.Properties.Application.Contracts.Persistence;
using Million.Properties.Domain.Entities;
using Million.Properties.Application.DTOs;


namespace Million.Properties.Infrastructure.Persistence.Repositories;

public class PropertyRepository(IMongoDatabase db) : IPropertyRepository
{
    private readonly IMongoCollection<Property> _collection = db.GetCollection<Property>("properties");

    public async Task<Property?> GetByIdAsync(string id) =>
        await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<IEnumerable<Property>> GetAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task AddAsync(Property property) =>
        await _collection.InsertOneAsync(property);

    public async Task UpdateAsync(Property property) =>
        await _collection.ReplaceOneAsync(x => x.Id == property.Id, property);

    public async Task DeleteAsync(string id) =>
        await _collection.DeleteOneAsync(x => x.Id == id);
}
