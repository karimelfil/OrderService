using Dapper;
using OrderService.Business.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Model;
using OrderService.Persistence.Contexts;
using System.Text.Json;

namespace OrderService.Persistence.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DapperContext _context;

        public OrderItemRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<ProcedureResult<OrderItemIdOnly>> CreateAsync(OrderItem item)
        {
            using var connection = _context.CreateConnection();

            var parameters = new
            {
                p_order_id = item.OrderId,
                p_product_name = item.ProductName,
                p_quantity = item.Quantity,
                p_price = item.Price,
                p_created_by = item.CreatedBy
            };

            var sql = "SELECT orders.sp_create_order_item(@p_order_id, @p_product_name, @p_quantity, @p_price, @p_created_by)";
            var jsonResult = await connection.QueryFirstOrDefaultAsync<string>(sql, parameters);

            Console.WriteLine($"Create OrderItem JSON: {jsonResult}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<ProcedureResult<OrderItemIdOnly>>(jsonResult, options);
        }

        public async Task<ProcedureResult<OrderItem>> GetByIdAsync(int id)
        {
            using var connection = _context.CreateConnection();

            var parameters = new { p_item_id = id };
            var sql = "SELECT orders.sp_get_order_item_by_id(@p_item_id)";
            var jsonResult = await connection.QueryFirstOrDefaultAsync<string>(sql, parameters);

            Console.WriteLine($"Get OrderItem JSON: {jsonResult}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<ProcedureResult<OrderItem>>(jsonResult, options);
        }

        public async Task<ProcedureResult<List<OrderItem>>> GetAllAsync()
        {
            using var connection = _context.CreateConnection();

            var sql = "SELECT orders.sp_get_all_order_items()";
            var jsonResult = await connection.QueryFirstOrDefaultAsync<string>(sql);

            Console.WriteLine($"Get All OrderItems JSON: {jsonResult}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<ProcedureResult<List<OrderItem>>>(jsonResult, options);
        }

        public async Task<ProcedureResult<bool>> UpdateAsync(OrderItem item)
        {
            using var connection = _context.CreateConnection();

            var parameters = new
            {
                p_item_id = item.Id,
                p_product_name = item.ProductName,
                p_quantity = item.Quantity,
                p_price = item.Price,
                p_updated_by = item.UpdatedBy
            };

            var sql = "SELECT orders.sp_update_order_item(@p_item_id, @p_product_name, @p_quantity, @p_price, @p_updated_by)";
            var jsonResult = await connection.QueryFirstOrDefaultAsync<string>(sql, parameters);

            Console.WriteLine($"Update OrderItem JSON: {jsonResult}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<ProcedureResult<bool>>(jsonResult, options);
        }

        public async Task<ProcedureResult<bool>> DeleteAsync(int id, string deletedBy)
        {
            using var connection = _context.CreateConnection();

            var parameters = new
            {
                p_item_id = id,
                p_deleted_by = deletedBy
            };

            var sql = "SELECT orders.sp_delete_order_item(@p_item_id, @p_deleted_by)";
            var jsonResult = await connection.QueryFirstOrDefaultAsync<string>(sql, parameters);

            Console.WriteLine($"Delete OrderItem JSON: {jsonResult}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<ProcedureResult<bool>>(jsonResult, options);
        }
    }
}

