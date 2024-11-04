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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IProductRepository productRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProductReadDto> CreateAsync(ProductCreateDto productCreateDto)
        {
            var sellerId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var product = _mapper.Map<Product>(productCreateDto);

            product.CreateUser = sellerId;
            product.CreateDate = DateTime.UtcNow;

            await _productRepository.AddAsync(product);
            
            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }

        public async Task<ProductReadDto> GetByIdAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<ProductReadDto> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var updateUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var product = await _productRepository.GetByIdAsync(productUpdateDto.Id);
            _mapper.Map(productUpdateDto, product);
            product.ModifiedUser = updateUser;
            await _productRepository.UpdateAsync(product);
            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task DeleteAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var deleteUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            product.DeletedUser = deleteUser;
            product.DeletedDate = DateTime.UtcNow;
            await _productRepository.UpdateAsync(product);
            await _productRepository.DeleteAsync(product);
        }
    }
}
