using Dapper;
using OrderService.Business.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Model;
using OrderService.Persistence.Contexts;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrderService.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _context;

        public OrderRepository(DapperContext context)
        {
            _context = context;
        }

        // create order
        public async Task<ProcedureResult<OrderIdOnly>> CreateOrderAsync(Order order)
        {
            using var connection = _context.CreateConnection();

            var parameters = new
            {
                p_customer_name = order.CustomerName,
                p_total_amount = order.Total,
                p_created_by = order.CreatedBy
            };

            var sql = "SELECT orders.sp_create_order(@p_customer_name, @p_total_amount, @p_created_by)";

            var jsonResult = await connection.QueryFirstOrDefaultAsync<string>(sql, parameters);


             Console.WriteLine("Stored Procedure JSON: " + jsonResult);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,

            };
            Console.WriteLine($"Raw JSON from DB: {jsonResult}");
            return JsonSerializer.Deserialize<ProcedureResult<OrderIdOnly>>(jsonResult, options);
        }


        // get order by id 
        public async Task<ProcedureResult<Order>> GetOrderByIdAsync(int id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new { p_order_id = id };
                var sql = "SELECT orders.sp_get_order_by_id(@p_order_id)";
                var jsonResult = await connection.QueryFirstOrDefaultAsync<string>(sql, parameters);


                Console.WriteLine($"Raw JSON from DB: {jsonResult}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<ProcedureResult<Order>>(jsonResult, options);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error getting order by ID: {ex.Message}");
                return new ProcedureResult<Order>
                {
                    ErrorCode = 1,
                    Message = "Error retrieving order",
                    Data = null
                };
            }
        }


        // LiST ALL Orders 
        public async Task<ProcedureResult<List<Order>>> GetAllOrdersAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT orders.sp_get_all_orders()";
                var jsonResult = await connection.QueryFirstOrDefaultAsync<string>(sql);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                return JsonSerializer.Deserialize<ProcedureResult<List<Order>>>(jsonResult, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all orders: {ex.Message}");
                return new ProcedureResult<List<Order>>
                {
                    ErrorCode = 1,
                    Message = "Error retrieving orders",
                    Data = null
                };
            }
        }

        // Update Order
        public async Task<ProcedureResult<bool>> UpdateOrderAsync(Order order)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new
                {
                    p_order_id = order.Id,
                    p_customer_name = order.CustomerName,
                    p_total_amount = order.Total,
                    p_updated_by = order.UpdatedBy
                };

                var sql = "SELECT orders.sp_update_order(@p_order_id, @p_customer_name, @p_total_amount, @p_updated_by)";
                var jsonResult = await connection.QueryFirstOrDefaultAsync<string>(sql, parameters);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<ProcedureResult<bool>>(jsonResult, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating order: {ex.Message}");
                return new ProcedureResult<bool>
                {
                    ErrorCode = 1,
                    Message = "Error updating order",
                    Data = false
                };
            }
        }


        // Delete Order 
        public async Task<ProcedureResult<bool>> DeleteOrderAsync(int id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var parameters = new { p_order_id = id };
                var sql = "SELECT orders.sp_delete_order(@p_order_id)";

                var jsonResult = await connection.QueryFirstOrDefaultAsync<string>(sql, parameters);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<ProcedureResult<bool>>(jsonResult, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting order: {ex.Message}");
                return new ProcedureResult<bool>
                {
                    ErrorCode = 1,
                    Message = "Error deleting order",
                    Data = false
                };
            }
        }


    }
}
