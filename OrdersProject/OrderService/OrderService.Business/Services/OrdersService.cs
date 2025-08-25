using OrderService.Business.Interfaces;
using OrderService.Domain.Model.Order;


namespace OrderService.Business.Services
{
    public class OrdersService : IOrderService
    {
        private readonly IServiceRepository _repository;

        public OrdersService(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateOrderResponseDto> CreateOrderAsync(CreateOrderRequestDto dto)
        {
            try
            {
                var customerId = await _repository.CreateCustomerAsync(
                    dto.CustomerFirstName, dto.CustomerLastName, dto.CustomerEmail, dto.CustomerPhone, dto.CreatedBy);

                var orderId = await _repository.CreateOrderAsync(customerId, DateTime.UtcNow, DateTime.UtcNow);

                var itemIds = new List<int>();

                foreach (var item in dto.Items)
                {
                    var itemId = await _repository.CreateItemAsync(item.Name, item.Description, item.Price, dto.CreatedBy, DateTime.UtcNow);
                    itemIds.Add(itemId);

                    await _repository.CreateOrderItemAsync(orderId, itemId, item.Quantity, item.Price, DateTime.UtcNow);
                }

                return new CreateOrderResponseDto
                {
                    CustomerId = customerId,
                    OrderId = orderId,
                    ItemIds = itemIds
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create order.", ex);
            }
        }

       
    }

}
