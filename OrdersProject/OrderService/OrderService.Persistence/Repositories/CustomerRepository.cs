using Microsoft.EntityFrameworkCore;
using OrderService.Business.Interfaces;
using OrderService.Domain.Entities;


namespace OrderService.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context) => _context = context;

        public async Task<List<Customer>> GetAllAsync() =>
            await _context.Customers.Where(i => i.IsActive).ToListAsync();

        public async Task<Customer?> GetByIdAsync(int id) => await _context.Customers.FindAsync(id);


        public async Task UpdateAsync(Customer customer)
        {
            customer.UpdatedDate = DateTime.UtcNow;
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id, string updatedBy)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                customer.IsActive = false;
                customer.UpdatedBy = updatedBy;
                customer.UpdatedDate = DateTime.UtcNow;
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
            }
        }

    }
}
