using AutoMapper;
using Microsoft.AspNetCore.Http;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Model.Entities;
using ShoppingApp.Services.Interfaces;
using ShoppingApp.Shared.Dtos.CreateDtos;
using ShoppingApp.Shared.Dtos.ReadDtos;
using ShoppingApp.Shared.Dtos.UpdateDtos;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ShoppingApp.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OrderReadDto> CreateAsync(OrderCreateDto orderCreateDto)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cart = await _cartRepository.GetByIdAsync(orderCreateDto.CartId);
            if (cart == null) return null;

            var order = _mapper.Map<Order>(orderCreateDto);
            order.Carts = cart;
            order.CreateUser = userId;
            order.CreateDate = DateTime.UtcNow;

            await _orderRepository.AddAsync(order);
            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
        }

        public async Task<OrderReadDto> GetByIdAsync(string id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<OrderReadDto> UpdateAsync(OrderUpdateDto orderUpdateDto)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var order = await _orderRepository.GetByIdAsync(orderUpdateDto.Id);
            if (order == null) return null;

            _mapper.Map(orderUpdateDto, order);
            order.ModifiedUser = userId;
            order.ModifiedDate = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);
            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task DeleteAsync(string id)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return;

            order.DeletedUser = userId;
            order.DeletedDate = DateTime.UtcNow;
            await _orderRepository.UpdateAsync(order);
        }
    }
}
