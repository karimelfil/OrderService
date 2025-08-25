using OrderService.Domain.Entities;


namespace OrderService.Business.Interfaces
{
    public interface IServiceRepository
    {
        Task<int> CreateCustomerAsync(string firstName, string lastName, string email, string phone, string createdBy);
        Task<int> CreateOrderAsync(int customerId, DateTime orderDate, DateTime createdDate);
        Task<int> CreateItemAsync(string name, string description, decimal price, string createdBy, DateTime createdDate);
        Task CreateOrderItemAsync(int orderId, int itemId, int quantity, decimal unitPrice, DateTime createdDate);


        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int customerId);




    }

}
