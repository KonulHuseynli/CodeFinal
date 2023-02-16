using Web.Areas.Admin.ViewModels.ProductCategory;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IProductCategoryService
    {

        Task<ProductCategoryIndexVM> GetAllAsync();

        Task<bool> CreateAsync(ProductCategoryCreateVM model);
        Task<ProductCategoryUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(ProductCategoryUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
