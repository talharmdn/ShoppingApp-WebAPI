using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Interfaces;
using ShoppingApp.Shared.Dtos.CreateDtos;
using ShoppingApp.Shared.Dtos.ReadDtos;
using ShoppingApp.Shared.Dtos.UpdateDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto orderCreateDto)
        {
            var createdOrder = await _orderService.CreateAsync(orderCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReadDto>> GetById(string id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> Update([FromBody] OrderUpdateDto orderUpdateDto)
        {
            var updatedOrder = await _orderService.UpdateAsync(orderUpdateDto);
            return Ok(updatedOrder);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> Delete(string id)
        {
            await _orderService.DeleteAsync(id);
            return NoContent();
        }
    }
}
