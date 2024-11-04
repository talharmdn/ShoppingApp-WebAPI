using ShoppingApp.Shared.Dtos.CreateDtos;
using ShoppingApp.Shared.Dtos.ReadDtos;
using ShoppingApp.Shared.Dtos.UpdateDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingApp.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartReadDto> CreateAsync(CartCreateDto cartCreateDto);
        Task<IEnumerable<CartReadDto>> GetAllAsync();
        Task<CartReadDto> GetByIdAsync(string id);
        Task<CartReadDto> UpdateAsync(CartUpdateDto cartUpdateDto);
        Task DeleteAsync(string id);
    }
}
