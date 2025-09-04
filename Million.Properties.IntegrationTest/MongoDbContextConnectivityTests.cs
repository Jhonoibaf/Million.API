using MongoDB.Bson;
using MongoDB.Driver;
using Million.Properties.Infrastructure.Persistence;
using NUnit.Framework;

namespace Million.Properties.IntegrationTest;

[TestFixture]
public class MongoConnectivityTests
{
    private MongoDbContext _dbContext = null!;

    [SetUp]
    public void Setup()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("million");
        _dbContext = new MongoDbContext(database);
    }

    [Test]
    public async Task CanPingMongo()
    {
        var result = await _dbContext
            .GetCollection<BsonDocument>("properties")
            .Database
            .RunCommandAsync((Command<BsonDocument>)"{ ping: 1 }");

        Assert.That(result.Contains("ok"), Is.True);
        Assert.That(result["ok"].ToDouble(), Is.EqualTo(1.0));
    }
}
