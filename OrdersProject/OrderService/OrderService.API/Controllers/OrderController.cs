using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Business.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Model;

namespace OrderService.API.Controllers
{

    [ApiController]

    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // Create Order
        [HttpPost]
        [Route("api/post/[controller]/CreateOrder")]
        //[Authorize(Policy = "RequireUserRole")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = new Order
            {
                CustomerName = orderDto.CustomerName,
                OrderDate = orderDto.OrderDate,
                Total = orderDto.Total,
                CreatedBy = orderDto.CreatedBy,
                CreatedDate = DateTime.UtcNow
            };

            var result = await _orderRepository.CreateOrderAsync(order);

            if (result.ErrorCode != 0)
                return StatusCode(500, result.Message);

            return Ok(result);
        }


        // Get Order by ID
        [HttpGet("api/get/Orders/GetOrderById/{id}")]
        [Authorize(Policy = "RequireUserRole")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderRepository.GetOrderByIdAsync(id);

            if (result == null || result.ErrorCode != 0)
            {
                return NotFound(new { message = result?.Message ?? "Order not found" });
            }

            return Ok(result.Data);
        }

        // Get All Orders

        [HttpGet("api/get/Orders/GetAllOrders")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderRepository.GetAllOrdersAsync();

            if (result == null || result.ErrorCode != 0)
            {
                return StatusCode(500, new { message = result?.Message ?? "Failed to retrieve orders" });
            }

            return Ok(result.Data ?? new List<Order>());
        }

        // Update Order
        [HttpPut("api/put/Orders/UpdateOrder/{id}")]
        [Authorize(Policy = "RequireUserRole")]

        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDto orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedOrder = new Order
            {
                Id = id,
                CustomerName = orderDto.CustomerName,
                Total = orderDto.Total,
                UpdatedBy = orderDto.CreatedBy,
                UpdatedDate = DateTime.UtcNow
            };

            var result = await _orderRepository.UpdateOrderAsync(updatedOrder);

            if (result == null || result.ErrorCode != 0)
            {
                return StatusCode(
                    result?.ErrorCode == 1 && result.Message.Contains("not found")
                        ? StatusCodes.Status404NotFound
                        : StatusCodes.Status500InternalServerError,
                    new { message = result?.Message ?? "Failed to update order" }
                );
            }

            return Ok(new
            {
                success = result.Data,
                message = result.Message
            });
        }

        // Delete Order
        [HttpDelete("api/delete/Orders/DeleteOrder/{id}")]
        [Authorize(Policy = "RequireAdminRole")]

        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderRepository.DeleteOrderAsync(id);

            if (result == null || result.ErrorCode != 0)
            {
                return StatusCode(
                    result?.ErrorCode == 1 && result.Message.Contains("not found")
                        ? StatusCodes.Status404NotFound
                        : StatusCodes.Status500InternalServerError,
                    new { message = result?.Message ?? "Failed to delete order" }
                );
            }

            return Ok(new
            {
                success = result.Data,
                message = result.Message
            });
        }

    }
}
