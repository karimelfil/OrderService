using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using OrderService.API.Controllers;
using OrderService.Business.Interfaces;
using OrderService.Domain.Model.Order;
namespace OrderService.Tests {

    public class CustomersControllerTests
    {
        private readonly Mock<ICustomerService> _serviceMock;
        private readonly CustomersController _controller;
        '
        public CustomersControllerTests()
        {
            _serviceMock = new Mock<ICustomerService>();
            _controller = new CustomersController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithData()
        {
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<CustomerResponseDto>
        {
            new CustomerResponseDto { CustomerId = 1, FirstName = "Ali", LastName = "Fares" }
        });

            var result = await _controller.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsAssignableFrom<IEnumerable<CustomerResponseDto>>(ok.Value);
            Assert.Single(data);
        }

        [Fact]
        public async Task GetById_ShouldReturnCustomer()
        {
            _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(new CustomerResponseDto
            {
                CustomerId = 1,
                FirstName = "Zain"
            });

            var result = await _controller.GetById(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            var customer = Assert.IsType<CustomerResponseDto>(ok.Value);
            Assert.Equal("Zain", customer.FirstName);
        }

        [Fact]
        public async Task Update_ShouldReturnSuccessMessage()
        {
            _serviceMock.Setup(s => s.UpdateAsync(1, It.IsAny<CustomerRequestDto>()))
                .ReturnsAsync("Customer updated successfully");

            var dto = new CustomerRequestDto
            {
                FirstName = "Test",
                LastName = "Updated",
                Email = "x@test.com",
                Phone = "000",
                UpdatedBy = "admin"
            };

            var result = await _controller.Update(1, dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Customer updated successfully", ok.Value.ToString());
        }

        [Fact]
        public async Task Delete_ShouldReturnSuccessMessage()
        {
            _serviceMock.Setup(s => s.DeleteAsync(1, "admin")).ReturnsAsync("Customer deleted successfully");

            var result = await _controller.Delete(1, "admin");

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Customer deleted successfully", ok.Value.ToString());
        }
    }





}




