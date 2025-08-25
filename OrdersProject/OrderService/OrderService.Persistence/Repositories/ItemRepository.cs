using Microsoft.EntityFrameworkCore;
using OrderService.Business.Interfaces;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Persistence.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;
        public ItemRepository(AppDbContext context) => _context = context;

        public async Task<List<Item>> GetAllAsync() =>
            await _context.Items.Where(i => i.IsActive).ToListAsync();

        public async Task<Item?> GetByIdAsync(int id) => await _context.Items.FindAsync(id);

        public async Task UpdateAsync(Item item)
        {
            item.UpdatedDate = DateTime.UtcNow;
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id, string updatedBy)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                item.IsActive = false;
                item.UpdatedBy = updatedBy;
                item.UpdatedDate = DateTime.UtcNow;
                _context.Items.Update(item);
                await _context.SaveChangesAsync();
            }
        }

    }
}
