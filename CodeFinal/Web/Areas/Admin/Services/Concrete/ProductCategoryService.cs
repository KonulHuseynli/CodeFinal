using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.ProductCategory;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ProductCategoryService :IProductCategoryService
    {

        private readonly ModelStateDictionary _modelState;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IActionContextAccessor actionContextAccessor)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<ProductCategoryIndexVM> GetAllAsync()
        {
            var model = new ProductCategoryIndexVM
            {
                ProductCategories = await _productCategoryRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<bool> CreateAsync(ProductCategoryCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _productCategoryRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda category mövcuddur");
                return false;
            }

            var productCategory = new ProductCategory
            {

                Title = model.Title,
                CreatedAt = DateTime.Now,


            };

            await _productCategoryRepository.CreateAsync(productCategory);
            return true;
        }

        public async Task<ProductCategoryUpdateVM> GetUpdateModelAsync(int id)
        {


            var faqCategory = await _productCategoryRepository.GetAsync(id);

            if (faqCategory == null) return null;

            var model = new ProductCategoryUpdateVM
            {
                Id = faqCategory.Id,
                Title = faqCategory.Title,
            };

            return model;

        }

        public async Task<bool> UpdateAsync(ProductCategoryUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _productCategoryRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda cateogry mövcuddur");
                return false;
            }

            var productCategory = await _productCategoryRepository.GetAsync(model.Id);

            if (productCategory != null)
            {
                productCategory.Id = model.Id;
                productCategory.Title = model.Title;
                productCategory.ModifiedAt = DateTime.Now;


                await _productCategoryRepository.UpdateAsync(productCategory);

            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var productCategory = await _productCategoryRepository.GetAsync(id);
            if (productCategory != null)
            {
                await _productCategoryRepository.DeleteAsync(productCategory);
                return true;
            }

            return false;
        }
    }
}
