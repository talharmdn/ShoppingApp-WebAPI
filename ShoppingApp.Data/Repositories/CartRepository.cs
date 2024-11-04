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
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Cart entity)
        {
            await _context.Carts.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task<List<Cart>> GetAllAsync()
        {
            return await _context.Carts
                        .Include(c => c.Items)
                        .ThenInclude(ci => ci.Product) 
                         .ToListAsync();
        }

        public async Task<Cart> GetByIdAsync(string id)
        {
            return await _context.Carts
                            .Include(c => c.Items)
                            .ThenInclude(ci => ci.Product)
                            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Cart entity)
        {
            _context.Carts.Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Cart entity)
        {
            _context.Carts.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<List<Cart>> FindAllAsync(Expression<Func<Cart, bool>> predicate)
        {
            return await _context.Carts.Where(predicate).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
