using OrderService.Domain.Model.Order;


namespace OrderService.Business.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerResponseDto>> GetAllAsync();
        Task<CustomerResponseDto> GetByIdAsync(int id);

        Task<string> UpdateAsync(int id, CustomerRequestDto dto);
        Task<string> DeleteAsync(int id, string updatedBy);

    }
}
