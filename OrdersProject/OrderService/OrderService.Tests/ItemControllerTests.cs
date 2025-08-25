using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderService.API.Controllers;
using OrderService.Business.Interfaces;
using OrderService.Domain.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests
{
    public class ItemControllerTests
    {
        private readonly Mock<IItemService> _serviceMock;
        private readonly ItemController _controller;

        public ItemControllerTests()
        {
            _serviceMock = new Mock<IItemService>();
            _controller = new ItemController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfItems()
        {
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<ItemResponseDto>
        {
            new ItemResponseDto { Id = 1, Name = "Mouse", Price = 50 }
        });

            var result = await _controller.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsAssignableFrom<IEnumerable<ItemResponseDto>>(ok.Value);
            Assert.Single(data);
        }

        [Fact]
        public async Task GetById_ShouldReturnItem()
        {
            _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(new ItemResponseDto
            {
                Id = 1,
                Name = "Keyboard",
                Price = 80
            });

            var result = await _controller.GetById(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<ItemResponseDto>(ok.Value);
            Assert.Equal("Keyboard", item.Name);
        }

        [Fact]
        public async Task Update_ShouldReturnSuccessMessage()
        {
            _serviceMock.Setup(s => s.UpdateAsync(1, It.IsAny<ItemRequestDto>()))
                .ReturnsAsync("Item updated successfully");

            var dto = new ItemRequestDto
            {
                Name = "Updated",
                Price = 999,
                Description = "New Desc",
                UpdatedBy = "admin"
            };

            var result = await _controller.Update(1, dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Item updated successfully", ok.Value.ToString());
        }

        [Fact]
        public async Task Delete_ShouldReturnSuccessMessage()
        {
            _serviceMock.Setup(s => s.DeleteAsync(1, "admin")).ReturnsAsync("Item deleted successfully (soft delete)");

            var result = await _controller.Delete(1, "admin");

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Item deleted successfully", ok.Value.ToString());
        }
    }
}

