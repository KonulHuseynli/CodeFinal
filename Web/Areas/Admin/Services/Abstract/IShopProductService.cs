using Web.Areas.Admin.ViewModels.Product;
using Web.Areas.Admin.ViewModels.ShopProduct;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IShopProductService
    {
        Task<ShopProductIndexVM> GetAllAsync();
        Task<ShopProductCreateVM> GetCreateModelAsync();
        Task<bool> CreateAsync(ShopProductCreateVM model);
        Task<ShopProductUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(ShopProductUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
