using AutoMapper;
using OrderService.Business.Interfaces;
using OrderService.Domain.Model.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;
        public CustomerService(ICustomerRepository repo) => _repo = repo;

        public async Task<IEnumerable<CustomerResponseDto>> GetAllAsync()
        {
            var customers = await _repo.GetAllAsync();
            return customers.Select(c => new CustomerResponseDto
            {
                CustomerId=c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone
            });
        }

        public async Task<CustomerResponseDto> GetByIdAsync(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) throw new Exception("Not found");

            return new CustomerResponseDto
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone
            };
        }

        public async Task<string> UpdateAsync(int id, CustomerRequestDto dto)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) throw new Exception("Customer not found");

            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.Email = dto.Email;
            customer.Phone = dto.Phone;
            customer.UpdatedBy = dto.UpdatedBy;

            await _repo.UpdateAsync(customer);

            return "Customer updated successfully";
        }

        public async Task<string> DeleteAsync(int id, string updatedBy)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) throw new Exception("Customer not found");

            await _repo.SoftDeleteAsync(id, updatedBy);
            return "Customer deleted successfully";
        }

    }

}
