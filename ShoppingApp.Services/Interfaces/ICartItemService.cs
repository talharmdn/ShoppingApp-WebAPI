using ShoppingApp.Shared.Dtos.CreateDtos;
using ShoppingApp.Shared.Dtos.ReadDtos;
using ShoppingApp.Shared.Dtos.UpdateDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingApp.Services.Interfaces
{
    public interface ICartItemService
    {
        Task<CartItemReadDto> CreateAsync(CartItemCreateDto cartItemCreateDto);
        Task<IEnumerable<CartItemReadDto>> GetAllAsync();
        Task<CartItemReadDto> GetByIdAsync(string id);
        Task<CartItemReadDto> UpdateAsync(CartItemUpdateDto cartItemUpdateDto);
        Task DeleteAsync(string id);
    }
}
