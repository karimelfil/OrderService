using OrderService.Domain.Model.Order;


namespace OrderService.Business.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemResponseDto>> GetAllAsync();
        Task<ItemResponseDto> GetByIdAsync(int id);

        Task<string> UpdateAsync(int id, ItemRequestDto dto);
        Task<string> DeleteAsync(int id, string updatedBy);

    }
}
