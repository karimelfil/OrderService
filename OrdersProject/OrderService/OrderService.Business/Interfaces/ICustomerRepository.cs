using OrderService.Domain.Entities;

namespace OrderService.Business.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);

        Task UpdateAsync(Customer customer);
        Task SoftDeleteAsync(int id, string updatedBy);

    }
}
