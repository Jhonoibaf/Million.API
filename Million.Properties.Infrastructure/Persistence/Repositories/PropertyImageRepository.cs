using Million.Properties.Application.Contracts.Persistence;
using Million.Properties.Infrastructure.Persistence;

using Million.Properties.Domain.Entities;
using MongoDB.Driver;

public class PropertyImageRepository : IPropertyImageRepository
{
    private readonly IMongoCollection<PropertyImage> _col;

    public PropertyImageRepository(MongoDbContext ctx)
    {
        _col = ctx.PropertyImages;
    }

    public async Task AddAsync(PropertyImage image)
    {
        await _col.InsertOneAsync(image);
    }

    public async Task<Dictionary<string, string?>> GetFirstByPropertyIdsAsync(IEnumerable<string> ids)
    {
        var idSet = ids.Distinct().ToList();
        if (!idSet.Any()) return new();

        var list = await _col.Find(x => idSet.Contains(x.IdProperty) && x.Enabled).ToListAsync();

        return list
            .GroupBy(x => x.IdProperty)
            .ToDictionary(g => g.Key, g => g.First().File);
    }
}
