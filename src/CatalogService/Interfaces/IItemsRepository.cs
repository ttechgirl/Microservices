using CatalogService.Models;

namespace CatalogService.Interfaces
{
    public interface IItemsRepository
    {
        Task<IReadOnlyCollection<Items>> GetAllAsync();
        Task<Items> GetAsync(Guid id);
        Task<ItemsDto> CreateAsync(CreateItemsRequest request);
        Task<ItemsDto> UpdateAsync(UpdateItemsRequest request);
        Task RemoveAsync(Guid id);
    }
}