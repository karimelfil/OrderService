using Moq;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using OrderService.Domain.Entities;
using OrderService.Business.Services;
using OrderService.Business.Interfaces;
using OrderService.Domain.Model.Order;

namespace OrderService.Tests { 

    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockRepo;
        private readonly CustomerService _service;

        public CustomerServiceTests()
        {
            _mockRepo = new Mock<ICustomerRepository>();
            _service = new CustomerService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllCustomers()
        {

            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, FirstName = "Alice", LastName = "Smith", Email = "alice@example.com", Phone = "123" },
                new Customer { CustomerId = 2, FirstName = "Bob", LastName = "Brown", Email = "bob@example.com", Phone = "456" }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(customers);


            var result = await _service.GetAllAsync();


            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.FirstName == "Alice");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCustomer_WhenFound()
        {

            var customer = new Customer { CustomerId = 1, FirstName = "Alice", LastName = "Smith", Email = "alice@example.com", Phone = "123" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);

            var result = await _service.GetByIdAsync(1);


            Assert.NotNull(result);
            Assert.Equal("Alice", result.FirstName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowException_WhenNotFound()
        {

            _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);


            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetByIdAsync(99));
            Assert.Equal("Not found", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateCustomer_WhenExists()
        {
     
            var customer = new Customer { CustomerId = 1, FirstName = "Old", LastName = "Name", Email = "old@example.com", Phone = "000" };

            var dto = new CustomerRequestDto
            {
                FirstName = "New",
                LastName = "Name",
                Email = "new@example.com",
                Phone = "111",
                UpdatedBy = "admin"
            };

            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);
            _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);


            var result = await _service.UpdateAsync(1, dto);

     
            Assert.Equal("Customer updated successfully", result);
            _mockRepo.Verify(r => r.UpdateAsync(It.Is<Customer>(c => c.FirstName == "New" && c.UpdatedBy == "admin")), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenCustomerNotFound()
        {
            
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);

            var dto = new CustomerRequestDto();

            
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.UpdateAsync(99, dto));
            Assert.Equal("Customer not found", ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSoftDelete_WhenCustomerExists()
        {

            var customer = new Customer { CustomerId = 1 };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(customer);
            _mockRepo.Setup(r => r.SoftDeleteAsync(1, "admin")).Returns(Task.CompletedTask);

 
            var result = await _service.DeleteAsync(1, "admin");

     
            Assert.Equal("Customer deleted successfully", result);
            _mockRepo.Verify(r => r.SoftDeleteAsync(1, "admin"), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenCustomerNotFound()
        {
           
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);

         
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.DeleteAsync(99, "admin"));
            Assert.Equal("Customer not found", ex.Message);
        }
    }
}
