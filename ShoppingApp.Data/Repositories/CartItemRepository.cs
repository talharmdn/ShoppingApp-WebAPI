using Microsoft.EntityFrameworkCore;
using ShoppingApp.Data;
using ShoppingApp.Data.Data;
using ShoppingApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShoppingApp.Data.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AppDbContext _context;

        public CartItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CartItem entity)
        {
            await _context.CartItems.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task<List<CartItem>> GetAllAsync()
        {
            return await _context.CartItems.Include(ci => ci.Product) 
                                            .ToListAsync();
        }

        public async Task<CartItem> GetByIdAsync(string id)
        {
            return await _context.CartItems.Include(ci => ci.Product) 
                                           .FirstOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task UpdateAsync(CartItem entity)
        {
            _context.CartItems.Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(CartItem entity)
        {
            _context.CartItems.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<List<CartItem>> FindAllAsync(Expression<Func<CartItem, bool>> predicate)
        {
            return await _context.CartItems.Where(predicate).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
