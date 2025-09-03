using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Million.Properties.Application.Contracts.Persistence;
using Million.Properties.Application.Features.Properties.Queries.GetAllProperties;
using Million.Properties.Application.Mappings;
using Million.Properties.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace Million.Properties.Application.UnitTest.Features.Properties.Querys;

[TestFixture]
public class GetAllPropertiesHandlerTests
{
    private Mock<IPropertyRepository> _propRepo = null!;
    private Mock<IPropertyImageRepository> _imgRepo = null!;
    private IMapper _mapper = null!;

    [SetUp]
    public void SetUp()
    {
        _propRepo = new Mock<IPropertyRepository>();
        _imgRepo = new Mock<IPropertyImageRepository>();

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg => { cfg.AddProfile<MappingProfile>(); }, AppDomain.CurrentDomain.GetAssemblies());

        var provider = services.BuildServiceProvider();
        _mapper = provider.GetRequiredService<IMapper>();
    }

    [Test]
    public async Task GetAll_ReturnsMappedDtos_AndOneImagePerProperty()
    {
        var props = new List<Property>
        {
            new() { IdProperty = "p1", Name="A", Address="X", Price=1000, InternalCode=System.Guid.NewGuid().ToString() },
            new() { IdProperty = "p2", Name="B", Address="Y", Price=2000, InternalCode=System.Guid.NewGuid().ToString() },
        };

        _propRepo.Setup(r => r.GetAllAsync(null, null, null, null))
                 .ReturnsAsync(props);

        _imgRepo.Setup(r => r.GetFirstByPropertyIdsAsync(It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(new Dictionary<string, string?> {
                    { "p1", "img1" },
                    { "p2", null }
                });

        var handler = new GetAllPropertiesHandler(_propRepo.Object, _imgRepo.Object, _mapper);
        var result = await handler.Handle(new GetAllPropertiesQuery(new Domain.Entities.Request.GetAllPropertiesRequest()), CancellationToken.None);
        var resultList = result.ToList();
        Assert.That(resultList, Has.Count.EqualTo(2));
        Assert.That(resultList[0].File, Is.EqualTo("img1"));
    }
}
