using ShoppingApp.Shared.Dtos.CreateDtos;
using ShoppingApp.Shared.Dtos.ReadDtos;
using ShoppingApp.Shared.Dtos.UpdateDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderReadDto> CreateAsync(OrderCreateDto orderCreateDto);
        Task<IEnumerable<OrderReadDto>> GetAllAsync();
        Task<OrderReadDto> GetByIdAsync(string id);
        Task<OrderReadDto> UpdateAsync(OrderUpdateDto orderUpdateDto);
        Task DeleteAsync(string id);
    }
}
