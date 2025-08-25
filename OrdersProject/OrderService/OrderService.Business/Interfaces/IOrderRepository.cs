using OrderService.Domain.Entities;
using OrderService.Domain.Model;

namespace OrderService.Business.Interfaces
{
    public interface IOrderRepository
    {
        Task<ProcedureResult<OrderIdOnly>> CreateOrderAsync(Order order);
        Task<ProcedureResult<Order>> GetOrderByIdAsync(int id);
        Task<ProcedureResult<List<Order>>> GetAllOrdersAsync();
        Task<ProcedureResult<bool>> UpdateOrderAsync(Order order);
        Task<ProcedureResult<bool>> DeleteOrderAsync(int id);

    }
}
