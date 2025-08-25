using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Business.Interfaces;
using OrderService.Domain.Model.Order;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class ServiceController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(IOrderService orderService, ILogger<ServiceController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }
        // create order with cutomer and item 
        [HttpPost("orders")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto request)
        {
            try
            {
                var result = await _orderService.CreateOrderAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create order");

                return StatusCode(500, new
                {
                    message = "An error occurred while creating the order.",
                    details = ex.ToString() 
                });
            }
        }



    }
}



