using Microsoft.AspNetCore.Mvc;
using OrderService.Business.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Model;

namespace OrderService.API.Controllers
{
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemRepository _repository;

        public OrderItemsController(IOrderItemRepository repository)
        {
            _repository = repository;
        }


        // create orderItem 
        [HttpPost]
        [Route("api/post/orders/{orderId}/items/CreateItem")]
        public async Task<IActionResult> CreateItem(int orderId, [FromBody] CreateOrderItemDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = new OrderItem
            {
                OrderId = orderId,
                ProductName = dto.ProductName,
                Quantity = dto.Quantity,
                Price = dto.Price,
                CreatedBy = dto.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };

            var result = await _repository.CreateAsync(item);

            if (result.ErrorCode != 0)
            {
                return BadRequest(new { message = result.Message });
            }

            return CreatedAtAction(
                nameof(GetItemById),
                new { orderId, itemId = result.Data.ItemId },
                new
                {
                    itemId = result.Data.ItemId,
                    message = result.Message
                });
        }


        // get Itemorder by id
        [HttpGet("api/get/items/{itemId}/GetItemById")]
        public async Task<IActionResult> GetItemById(int orderId, int itemId)
        {
            var result = await _repository.GetByIdAsync(itemId);

            if (result.ErrorCode != 0 || result.Data == null)
                return NotFound(new { message = result.Message ?? "Order item not found" });

            if (result.Data.OrderId != orderId)
                return NotFound(new { message = "Order item not found for specified order" });

            return Ok(result.Data);
        }

        // get all item order 
        [HttpGet("api/get/items/GetAllItems")]


        public async Task<IActionResult> GetAllItems(int orderId)
        {
            var result = await _repository.GetAllAsync();

            if (result.ErrorCode != 0)
                return StatusCode(500, new { message = result.Message });


            var items = result.Data?.FindAll(x => x.OrderId == orderId) ?? new List<OrderItem>();

            return Ok(items);
        }

        // update item 
        [HttpPut("api/put/items/{itemId}/UpdateItem")]

        public async Task<IActionResult> UpdateItem(int orderId, int itemId, [FromBody] UpdateOrderItemDto dto)
        {
            var existingItem = await _repository.GetByIdAsync(itemId);
            if (existingItem.ErrorCode != 0 || existingItem.Data == null)
                return NotFound(new { message = "Order item not found" });

            if (existingItem.Data.OrderId != orderId)
                return NotFound(new { message = "Order item not found for specified order" });

            var item = new OrderItem
            {
                Id = itemId,
                OrderId = orderId,
                ProductName = dto.ProductName,
                Quantity = dto.Quantity,
                Price = dto.Price,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate = DateTime.UtcNow
            };

            var result = await _repository.UpdateAsync(item);

            if (result.ErrorCode != 0)
                return BadRequest(new { message = result.Message });

            return Ok(new { success = result.Data, message = result.Message });
        }


        // delte item 
        [HttpDelete("api/get/items/{itemId}/DeleteItem")]
        public async Task<IActionResult> DeleteItem(int orderId, int itemId, [FromBody] string deletedBy)
        {
            var existingItem = await _repository.GetByIdAsync(itemId);
            if (existingItem.ErrorCode != 0 || existingItem.Data == null)
                return NotFound(new { message = "Order item not found" });

            if (existingItem.Data.OrderId != orderId)
                return NotFound(new { message = "Order item not found for specified order" });

            var result = await _repository.DeleteAsync(itemId, deletedBy);

            if (result.ErrorCode != 0)
                return BadRequest(new { message = result.Message });

            return Ok(new { success = result.Data, message = result.Message });
        }
    }

}
