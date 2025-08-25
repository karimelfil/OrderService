using OrderService.Domain.Entities;


namespace OrderService.Business.Interfaces
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllAsync();
        Task<Item?> GetByIdAsync(int id);

        Task UpdateAsync(Item item);
        Task SoftDeleteAsync(int id, string updatedBy);

    }
}
