using Core.Entities;
using Web.Areas.Admin.ViewModels.ShopProduct;
using Web.ViewModels.Shop;

namespace Web.Services.Abstract
{
    public interface IShopService
    {
        Task<ShopIndexVM> GetAllAsync(ShopIndexVM model);
        Task<ShopProductIndexVM> CategoryProductAsync(int id);

        IQueryable<ShopProduct> FilterProducts(ShopIndexVM model);
    }
}
