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
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        private readonly AppDbContext _context;

        public ProductCategoryRepository(AppDbContext context) : base(context) 
        
        {
            _context = context;
        }

        public async Task<List<ProductCategory>> GetAllCategoryAsync()
        {
            return await _context.productCategories.Include(p => p.Products).ToListAsync();
        }

        public async Task<ProductCategory> GetFirstAsync()
        {
            return await _context.productCategories.FirstOrDefaultAsync();
        }

    }
}
