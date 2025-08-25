using OrderService.Domain.Model.Order;


namespace OrderService.Business.Interfaces
{
    public interface IOrderService
    {
        Task<CreateOrderResponseDto> CreateOrderAsync(CreateOrderRequestDto dto);

    }
}
