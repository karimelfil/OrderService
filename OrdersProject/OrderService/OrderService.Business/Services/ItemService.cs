using OrderService.Business.Interfaces;
using OrderService.Domain.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Business.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repo;
        public ItemService(IItemRepository repo) => _repo = repo;

        public async Task<IEnumerable<ItemResponseDto>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.Select(i => new ItemResponseDto
            {
                Id = i.ItemId,
                Name = i.Name,
                Price = i.Price,


            });
        }

        public async Task<ItemResponseDto> GetByIdAsync(int id)
        {
            var i = await _repo.GetByIdAsync(id);
            if (i == null) throw new Exception("Not found");

            return new ItemResponseDto
            {
                Id = i.ItemId,
                Name = i.Name,
                Price = i.Price,


            };
        }

        public async Task<string> UpdateAsync(int id, ItemRequestDto dto)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) throw new Exception("Item not found");

            item.Name = dto.Name;
            item.Description = dto.Description;
            item.Price = dto.Price;
            item.UpdatedBy = dto.UpdatedBy;

            await _repo.UpdateAsync(item);
            return "Item updated successfully";
        }

        public async Task<string> DeleteAsync(int id, string updatedBy)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) throw new Exception("Item not found");

            await _repo.SoftDeleteAsync(id, updatedBy);
            return "Item deleted successfully (soft delete)";
        }

    }

}
