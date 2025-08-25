using Moq;
using OrderService.Business.Interfaces;
using OrderService.Business.Services;
using OrderService.Domain.Entities;
using OrderService.Domain.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests
{

    public class ItemServiceTests
    {
        private readonly Mock<IItemRepository> _repoMock;
        private readonly ItemService _service;

        public ItemServiceTests()
        {
            _repoMock = new Mock<IItemRepository>();
            _service = new ItemService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnItems()
        {
            var items = new List<Item>
        {
            new Item { ItemId = 1, Name = "Chair", Price = 100 },
            new Item { ItemId = 2, Name = "Table", Price = 250 }
        };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(items);

            var result = await _service.GetAllAsync();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, x => x.Name == "Chair");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnItem()
        {
            var item = new Item { ItemId = 1, Name = "Laptop", Price = 1200 };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(item);

            var result = await _service.GetByIdAsync(1);

            Assert.Equal("Laptop", result.Name);
            Assert.Equal(1200, result.Price);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnSuccessMessage()
        {
            var item = new Item { ItemId = 1, Name = "Old", Price = 10 };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(item);

            var dto = new ItemRequestDto
            {
                Name = "Updated",
                Price = 99,
                Description = "Updated desc",
                UpdatedBy = "admin"
            };

            var msg = await _service.UpdateAsync(1, dto);

            _repoMock.Verify(r => r.UpdateAsync(It.Is<Item>(i =>
                i.Name == "Updated" && i.UpdatedBy == "admin")), Times.Once);

            Assert.Equal("Item updated successfully", msg);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccessMessage()
        {
            var item = new Item { ItemId = 1, Name = "ToDelete" };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(item);

            var msg = await _service.DeleteAsync(1, "admin");

            _repoMock.Verify(r => r.SoftDeleteAsync(1, "admin"), Times.Once);
            Assert.Equal("Item deleted successfully (soft delete)", msg);
        }
    }
}
