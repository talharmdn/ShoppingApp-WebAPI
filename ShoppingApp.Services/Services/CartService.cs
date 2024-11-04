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

namespace ShoppingApp.Services.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartItemRepository _cartItemRepository;


        public CartService(ICartRepository cartRepository, ICartItemRepository cartItemRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _cartItemRepository = cartItemRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CartReadDto> CreateAsync(CartCreateDto cartCreateDto)
        {
            var cart = new Cart
            {
                Id = Guid.NewGuid().ToString(),
                CreateDate = DateTime.UtcNow,
                CreateUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Items = new List<CartItem>()
            };

            foreach (var cartItemId in cartCreateDto.CartItemId)
            {
                var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
                if (cartItem == null)
                {
                    throw new Exception($"CartItem with ID {cartItemId} not found");
                }

                cart.Items.Add(new CartItem
                {
                    CartId = cartItem.CartId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Product = cartItem.Product,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                });
            }

            await _cartRepository.AddAsync(cart);

            return new CartReadDto
            {
                Id = cart.Id,
                Items = cart.Items.Select(ci => new CartItemReadDto
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    ProductName = ci.Product?.Name,
                    ProductPrice = ci.Product?.Price ?? 0,
                    CreateDate = ci.CreateDate,
                    CreateUser = ci.CreateUser
                }).ToList(),
                CreateDate = cart.CreateDate,
                CreateUser = cart.CreateUser
            };
        }



        private async Task<Cart> CreateCartAsync(string cartItemId)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
            if (cartItem == null)
            {
                throw new Exception("CartItem not found");
            }

            var cart = new Cart
            {
                Id = Guid.NewGuid().ToString(),
                CreateDate = DateTime.UtcNow,
                CreateUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Items = new List<CartItem>
        {
            new CartItem
            {
                CartId = cartItem.CartId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                Product = cartItem.Product, 
                CreateDate = DateTime.UtcNow,
                CreateUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            }
        }
            };

            await _cartRepository.AddAsync(cart);
            return cart;
        }


        public async Task<IEnumerable<CartReadDto>> GetAllAsync()
        {
            var carts = await _cartRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CartReadDto>>(carts);
        }

        public async Task<CartReadDto> GetByIdAsync(string id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            return _mapper.Map<CartReadDto>(cart);
        }

        public async Task<CartReadDto> UpdateAsync(CartUpdateDto cartUpdateDto)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = await _cartRepository.GetByIdAsync(cartUpdateDto.Id);

            if (cart == null)
            {
                throw new NotFoundException("Cart not found");
            }

            foreach (var itemUpdate in cartUpdateDto.Items)
            {
                var cartItem = cart.Items.FirstOrDefault(i => i.Id == itemUpdate.Id);
                if (cartItem != null)
                {
                    cartItem.Quantity = itemUpdate.Quantity; 
                }
            }

            cart.ModifiedUser = userId;
            cart.ModifiedDate = DateTime.UtcNow;
            await _cartRepository.UpdateAsync(cart);

            return _mapper.Map<CartReadDto>(cart);
        }


        public async Task DeleteAsync(string id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (cart != null)
            {
                cart.DeletedUser = userId;
                cart.DeletedDate = DateTime.UtcNow;
                await _cartRepository.UpdateAsync(cart);
                await _cartRepository.DeleteAsync(cart);
            }
        }

        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }
        }
    }
}
