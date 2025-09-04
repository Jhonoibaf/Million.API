using Million.Properties.Domain.Entities;

namespace Million.Properties.Application.Contracts.Persistence;

public interface IPropertyImageRepository
{
    Task AddAsync(PropertyImage image);
    Task<Dictionary<string, string?>> GetFirstByPropertyIdsAsync(IEnumerable<string> ids);
}
