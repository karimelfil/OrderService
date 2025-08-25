using Moq;
using OrderService.Business.Interfaces;
using OrderService.Business.Services;
using OrderService.Domain.Model;
using OrderService.Domain.Model.Order;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OrderService.Tests.Services
{
    public class OrdersServiceTests
    {
        private readonly Mock<IServiceRepository> _mockRepo;
        private readonly OrdersService _service;

        public OrdersServiceTests()
        {
            _mockRepo = new Mock<IServiceRepository>();
            _service = new OrdersService(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_ReturnsResponseDto_WhenSuccess()
        {

            var dto = new CreateOrderRequestDto
            {
                CustomerFirstName = "John",
                CustomerLastName = "Doe",
                CustomerEmail = "john@example.com",
                CustomerPhone = "123456789",
                CreatedBy = "admin",
                Items = new List<CreateOrdersItemDto>
                {
                    new CreateOrdersItemDto { Name = "Item1", Description = "desc", Price = 100, Quantity = 2 }
                }
            };

            _mockRepo.Setup(r => r.CreateCustomerAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                     .ReturnsAsync(1);

            _mockRepo.Setup(r => r.CreateOrderAsync(1, It.IsAny<System.DateTime>(), It.IsAny<System.DateTime>()))
                     .ReturnsAsync(1);

            _mockRepo.Setup(r => r.CreateItemAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<System.DateTime>()))
                     .ReturnsAsync(1);

            _mockRepo.Setup(r => r.CreateOrderItemAsync(1, 1, 2, 100, It.IsAny<System.DateTime>()))
                     .Returns(Task.CompletedTask);

           
            var result = await _service.CreateOrderAsync(dto);

      
            Assert.Equal(1, result.CustomerId);
            Assert.Equal(1, result.OrderId);
            Assert.Contains(1, result.ItemIds);
        }
    }
}
