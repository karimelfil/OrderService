using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Business.Interfaces;
using OrderService.Domain.Model.Order;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService) => _itemService = itemService;

        // get all items 
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _itemService.GetAllAsync();
            return Ok(result);
        }

        // get item by id 
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _itemService.GetByIdAsync(id);
            return Ok(result);
        }


        // update item 
        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemRequestDto dto)
        {
            var message = await _itemService.UpdateAsync(id, dto);
            return Ok(new { message });
        }

        // delete item 
        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Delete(int id, [FromQuery] string updatedBy)
        {
            var message = await _itemService.DeleteAsync(id, updatedBy);
            return Ok(new { message });
        }

    }
}
