using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OrderService.API.Controllers;
using OrderService.Business.Interfaces;
using OrderService.Domain.Model;
using OrderService.Domain.Model.Order;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OrderService.Tests
{
    public class ServiceControllerTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly ServiceController _controller;

        public ServiceControllerTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            var logger = new LoggerFactory().CreateLogger<ServiceController>();
            _controller = new ServiceController(_mockOrderService.Object, logger);
        }

        [Fact]
        public async Task CreateOrder_ReturnsOkResult_WhenOrderIsCreated()
        {
            // Arrange
            var request = new CreateOrderRequestDto
            {
                CustomerFirstName = "John",
                CustomerLastName = "Doe",
                CustomerEmail = "john.doe@example.com",
                CustomerPhone = "1234567890",
                CreatedBy = "tester",
                Items = new List<CreateOrdersItemDto>
                {
                    new CreateOrdersItemDto
                    {
                        Name = "Item1",
                        Description = "Test item",
                        Price = 100,
                        Quantity = 2
                    }
                }
            };

            var response = new CreateOrderResponseDto
            {
                CustomerId = 1,
                OrderId = 101,
                ItemIds = new List<int> { 10 }
            };

            _mockOrderService.Setup(s => s.CreateOrderAsync(request))
                             .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateOrder(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedData = Assert.IsType<CreateOrderResponseDto>(okResult.Value);

            Assert.Equal(1, returnedData.CustomerId);
            Assert.Equal(101, returnedData.OrderId);
            Assert.Single(returnedData.ItemIds);
            Assert.Equal(10, returnedData.ItemIds[0]);
        }
    }
}
