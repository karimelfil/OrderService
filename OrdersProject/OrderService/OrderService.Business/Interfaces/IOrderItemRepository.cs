using OrderService.Domain.Entities;
using OrderService.Domain.Model;


namespace OrderService.Business.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<ProcedureResult<OrderItemIdOnly>> CreateAsync(OrderItem item);
        Task<ProcedureResult<OrderItem>> GetByIdAsync(int id);
        Task<ProcedureResult<List<OrderItem>>> GetAllAsync();
        Task<ProcedureResult<bool>> UpdateAsync(OrderItem item);
        Task<ProcedureResult<bool>> DeleteAsync(int id, string deletedBy);
    }
}
