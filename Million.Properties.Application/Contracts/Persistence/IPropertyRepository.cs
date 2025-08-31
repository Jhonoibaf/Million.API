using Million.Properties.Domain.Entities;

namespace Million.Properties.Application.Contracts.Persistence
{
    public interface IPropertyRepository
    {
        Task<Property?> GetByIdAsync(string id);
        Task<IEnumerable<Property>> GetAllAsync();
        Task AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task DeleteAsync(string id);
    }
}
