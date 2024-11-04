using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Interfaces;
using ShoppingApp.Services.Services;
using ShoppingApp.Shared.Dtos.CreateDtos;
using ShoppingApp.Shared.Dtos.ReadDtos;
using ShoppingApp.Shared.Dtos.UpdateDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemsController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> Create([FromBody] CartItemCreateDto productCreateDto)
        {
            var createdProduct = await _cartItemService.CreateAsync(productCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemReadDto>>> GetAll()
        {
            var products = await _cartItemService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartItemReadDto>> GetById(string id)
        {
            var product = await _cartItemService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> Update([FromBody] CartItemUpdateDto productUpdateDto)
        {
            var updatedProduct = await _cartItemService.UpdateAsync(productUpdateDto);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> Delete(string id)
        {
            await _cartItemService.DeleteAsync(id);
            return NoContent();
        }
    }
}
