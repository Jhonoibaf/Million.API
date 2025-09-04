﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Million.Properties.Application.Contracts.Persistence;
using Million.Properties.Application.Features.Properties.Queries.GetPropertyById;
using Million.Properties.Application.Mappings;
using Million.Properties.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace Million.Properties.Application.UnitTest.Features.Properties.Querys;

[TestFixture]
public class GetPropertyByIdHandlerTests
{
    private Mock<IPropertyRepository> _propRepo = null!;
    private Mock<IPropertyImageRepository> _imgRepo = null!;
    private IMapper _mapper = null!;

    [SetUp]
    public void Setup()
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
    public async Task GetById_ReturnsDto_WithFile_WhenExists()
    {
        var propertyId = "p1";

        _propRepo.Setup(r => r.GetByIdAsync(propertyId)).ReturnsAsync(new Property
        {
            IdProperty = propertyId,
            Name = "A",
            Address = "X",
            Price = 10,
            InternalCode = Guid.NewGuid().ToString(),
            File = null 
        });

        _imgRepo.Setup(r => r.GetFirstByPropertyIdsAsync(It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(new Dictionary<string, string?> { { propertyId, "file.png" } });

        var handler = new GetPropertyByIdHandler(_propRepo.Object, _imgRepo.Object, _mapper);

        var dto = await handler.Handle(new GetPropertyByIdQuery(propertyId), CancellationToken.None);

        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.IdProperty, Is.EqualTo(propertyId));
        Assert.That(dto.File, Is.EqualTo("file.png"));
    }
}
