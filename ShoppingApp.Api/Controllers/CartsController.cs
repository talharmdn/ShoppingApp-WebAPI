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
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> Create([FromBody] CartCreateDto cartCreateDto)
        {
            var createdCart = await _cartService.CreateAsync(cartCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCart.Id }, createdCart);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartReadDto>>> GetAll()
        {
            var carts = await _cartService.GetAllAsync();
            return Ok(carts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartReadDto>> GetById(string id)
        {
            var cart = await _cartService.GetByIdAsync(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> Update([FromBody] CartUpdateDto cartUpdateDto)
        {
            var updatedCart = await _cartService.UpdateAsync(cartUpdateDto);
            return Ok(updatedCart);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> Delete(string id)
        {
            await _cartService.DeleteAsync(id);
            return NoContent();
        }
    }
}
