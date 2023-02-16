using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IShopProductRepository : IRepository<ShopProduct>
    {
        Task<List<ShopProduct>> GetAllGetCategoryAsync();
        Task<List<ShopProduct>> GetByCategoryIdAsync(int categoryId);

        IQueryable<ShopProduct> FilterByTitle(string title);
        Task<ShopProduct> GetProduct(int productId);
    }
}
