using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Business.Interfaces;
using OrderService.Domain.Model.Order;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        public CustomersController(ICustomerService service) => _service = service;
        


        // getall customer 
        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }


        //get customer by id 
        [HttpGet("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }


        //update customer 
        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerRequestDto dto)
        {
            var message = await _service.UpdateAsync(id, dto);
            return Ok(new { message });
        }


        // delete customer 
        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Delete(int id, [FromQuery] string updatedBy)
        {
            var message = await _service.DeleteAsync(id, updatedBy);
            return Ok(new { message });
        }

    }

}
