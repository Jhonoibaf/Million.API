using System.Reflection;
using Million.Properties.Infrastructure.Persistence;
using Million.Properties.Infrastructure.Persistence.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
public interface IDatabaseInitializer
{
    Task InitializeAsync(CancellationToken ct = default);
}

public sealed class DatabaseInitializer : IDatabaseInitializer
{
    private readonly IMongoDatabase _database;
    public DatabaseInitializer(IMongoDatabase db) => _database = db;

    public async Task InitializeAsync(CancellationToken ct = default)
    {
        var pack = new ConventionPack
        {
            new CamelCaseElementNameConvention(),
            new IgnoreIfNullConvention(true),
            new IgnoreExtraElementsConvention(true),
            new EnumRepresentationConvention(BsonType.String)
        };

        if (ConventionRegistry.Lookup(typeof(MongoDbContext)) == null)
            ConventionRegistry.Register("DefaultConventions", pack, _ => true);

        await _database.RunCommandAsync<BsonDocument>("{ ping: 1 }", cancellationToken: ct);

        var existing = await (await _database.ListCollectionNamesAsync(cancellationToken: ct))
                                        .ToListAsync(ct);

        async Task Ensure(string name)
        {
            if (!existing.Contains(name))
                await _database.CreateCollectionAsync(name, cancellationToken: ct);
        }

        await Ensure("owners");
        await Ensure("properties");
        await Ensure("propertyImages");
        await Ensure("propertyTraces");

        var props = _database.GetCollection<Property>("properties");
        await props.Indexes.CreateManyAsync(new[]
        {
            new CreateIndexModel<Property>(
                Builders<Property>.IndexKeys.Text(p => p.Name).Text(p => p.Address)),
            new CreateIndexModel<Property>(
                Builders<Property>.IndexKeys.Ascending(p => p.Price)),
            new CreateIndexModel<Property>(
                Builders<Property>.IndexKeys.Descending(p=>p.Price)),
            new CreateIndexModel<Property>(
                Builders<Property>.IndexKeys.Ascending(p => p.InternalCode),
                new CreateIndexOptions { Unique = true })
        }, cancellationToken: ct);
    }
}

