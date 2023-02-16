using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class ShopProductRepository : Repository<ShopProduct>, IShopProductRepository
    {
        private readonly AppDbContext _context;

        public ShopProductRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<ShopProduct>> GetAllGetCategoryAsync()
        {
            return await _context.shopProducts.Include(p => p.ProductCategory).ToListAsync();
        }

        public async Task<List<ShopProduct>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.shopProducts.Where(p => p.ProductCategoryId == categoryId).ToListAsync();
        }


        public IQueryable<ShopProduct> FilterByTitle(string title)
        {
            return _context.shopProducts.Where(p => !string.IsNullOrEmpty(title) ? p.Title.Contains(title) : true);
        }

        public async Task<ShopProduct> GetProduct(int productId)
        {
            var product = await _context.shopProducts.FirstOrDefaultAsync(p => p.Id == productId);

            return product;
        }
    }
   
}
