using AutoMapper;
using Microsoft.AspNetCore.Http;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Model.Entities;
using ShoppingApp.Services.Interfaces;
using ShoppingApp.Shared.Dtos.CreateDtos;
using ShoppingApp.Shared.Dtos.ReadDtos;
using ShoppingApp.Shared.Dtos.UpdateDtos;
using System.Security.Claims;

namespace ShoppingApp.Services.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository; 
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartItemService(ICartItemRepository cartItemRepository, IProductRepository productRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository; 
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CartItemReadDto> CreateAsync(CartItemCreateDto cartItemCreateDto)
        {
            var createUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var product = await _productRepository.GetByIdAsync(cartItemCreateDto.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var cartItem = new CartItem
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = product.Id,
                Quantity = cartItemCreateDto.Quantity,
                Product = product, 
                CreateUser = createUser,
                CreateDate = DateTime.UtcNow,
            };

            await _cartItemRepository.AddAsync(cartItem);

            return _mapper.Map<CartItemReadDto>(cartItem);
        }

        public async Task<IEnumerable<CartItemReadDto>> GetAllAsync()
        {
            var cartItems = await _cartItemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CartItemReadDto>>(cartItems);
        }

        public async Task<CartItemReadDto> GetByIdAsync(string id)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(id);
            return _mapper.Map<CartItemReadDto>(cartItem);
        }

        public async Task<CartItemReadDto> UpdateAsync(CartItemUpdateDto cartItemUpdateDto)
        {
            var updateUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemUpdateDto.Id);
            _mapper.Map(cartItemUpdateDto, cartItem);
            cartItem.ModifiedUser = updateUser;
            await _cartItemRepository.UpdateAsync(cartItem);
            return _mapper.Map<CartItemReadDto>(cartItem);
        }

        public async Task DeleteAsync(string id)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(id);
            var deleteUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            cartItem.DeletedUser = deleteUser;
            cartItem.DeletedDate = DateTime.UtcNow;
            await _cartItemRepository.UpdateAsync(cartItem);
            await _cartItemRepository.DeleteAsync(cartItem);
        }
    }
}
