using NZwalksApi.Models.Domain;

namespace NZwalksApi.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetByIdAsync(Guid id);

        Task<Region?> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid id, Region regions);

        Task<Region?> DeleteAsync(Guid id);
    }
}
