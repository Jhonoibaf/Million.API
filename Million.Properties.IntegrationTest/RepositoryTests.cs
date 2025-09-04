using Million.Properties.Domain.Entities;
using Million.Properties.Infrastructure.Persistence;
using Million.Properties.Infrastructure.Persistence.Repositories;
using MongoDB.Driver;
using NUnit.Framework;

namespace Million.Properties.IntegrationTest;

[TestFixture]
public class RepositoryTests
{
    private MongoDbContext _ctx = null!;
    private PropertyRepository _repo = null!;

    [SetUp]
    public void Setup()
    {
        var client = new MongoClient("mongodb://localhost:27017"); 
        var database = client.GetDatabase("million");              
        _ctx = new MongoDbContext(database);                       
        _repo = new PropertyRepository(_ctx);       
    }

    [Test]
    public async Task AddAndGetById_Works()
    {
        var p = new Property
        {
            Name = "Int Test House",
            Address = "Street 1",
            Price = 123m,
            InternalCode = Guid.NewGuid().ToString(),
        };
        await _repo.AddAsync(p);

        var found = await _repo.GetByIdAsync(p.IdProperty);
        Assert.That(found, Is.Not.Null);
        Assert.That(found!.Name, Is.EqualTo("Int Test House"));
    }

    [Test]
    public async Task Search_WithPriceRange_Works()
    {
        var list = await _repo.GetAllAsync(null, null, 0m, 1000000m);
        Assert.That(list, Is.Not.Null);
    }
}
