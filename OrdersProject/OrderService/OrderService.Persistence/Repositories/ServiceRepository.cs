using Dapper;
using OrderService.Business.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Persistence.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DapperContext _context;

        public ServiceRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateCustomerAsync(string firstName, string lastName, string email, string phone, string createdBy)
        {
            var sql = "SELECT orders.sp_create_customer(@FirstName, @LastName, @Email, @Phone, @CreatedBy, @CreatedDate)";
            using var connection = _context.CreateConnection();
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return await connection.ExecuteScalarAsync<int>(sql, new
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                CreatedBy = createdBy,
                CreatedDate = DateTime.UtcNow
            });
        }

        public async Task<int> CreateOrderAsync(int customerId, DateTime orderDate, DateTime createdDate)
        {
            var sql = "SELECT orders.sp_create_orderr(@CustomerId, @OrderDate, @CreatedDate)";
            using var connection = _context.CreateConnection();
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return await connection.ExecuteScalarAsync<int>(sql, new
            {
                CustomerId = customerId,
                OrderDate = orderDate,
                CreatedDate = createdDate
            });
        }

        public async Task<int> CreateItemAsync(string name, string description, decimal price, string createdBy, DateTime createdDate)
        {
            var sql = "SELECT orders.sp_create_item(@Name, @Description, @Price, @CreatedBy, @CreatedDate)";
            using var connection = _context.CreateConnection();
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return await connection.ExecuteScalarAsync<int>(sql, new
            {
                Name = name,
                Description = description,
                Price = price,
                CreatedBy = createdBy,
                CreatedDate = createdDate
            });
        }

        public async Task CreateOrderItemAsync(int orderId, int itemId, int quantity, decimal unitPrice, DateTime createdDate)
        {
            var sql = "SELECT orders.sp_create_order_items(@OrderId, @ItemId, @Quantity, @UnitPrice, @CreatedDate)";
            using var connection = _context.CreateConnection();
            if (connection.State != ConnectionState.Open)
                connection.Open();

            await connection.ExecuteAsync(sql, new
            {
                OrderId = orderId,
                ItemId = itemId,
                Quantity = quantity,
                UnitPrice = unitPrice,
                CreatedDate = createdDate
            });
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            const string sql = "SELECT * FROM orders.\"Customer\" WHERE \"IsActive\" = true";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Customer>(sql);
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            const string sql = "SELECT * FROM orders.\"Customer\" WHERE \"CustomerId\" = @CustomerId AND \"IsActive\" = true";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Customer>(sql, new { CustomerId = customerId });
        }


    }
}
