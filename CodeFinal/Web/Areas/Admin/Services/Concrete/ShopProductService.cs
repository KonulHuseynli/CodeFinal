using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Product;
using Web.Areas.Admin.ViewModels.ShopProduct;
using Web.ViewModels.Shop;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ShopProductService :IShopProductService
    {
        private readonly IShopProductRepository _shopProductRepository;
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly IFileService _fileService;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ModelStateDictionary _modelState;
        public ShopProductService(IShopProductRepository shopProductRepository,
            IActionContextAccessor actionContextAccessor,
            IFileService fileService,
            IProductCategoryRepository productCategoryRepository)
        {
            _shopProductRepository = shopProductRepository;
            _fileService = fileService;
            _productCategoryRepository = productCategoryRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<ShopProductIndexVM> GetAllAsync()
        {
            var model = new ShopProductIndexVM
            {
                Products = await _shopProductRepository.GetAllGetCategoryAsync()
            };
            return model;

        }
        public async Task<ShopProductCreateVM> GetCreateModelAsync()
        {
            var categories = await _productCategoryRepository.GetAllAsync();
            var model = new ShopProductCreateVM
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToList()
            };

            return model;
        }

        public async Task<bool> CreateAsync(ShopProductCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _shopProductRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda Product mövcuddur");
                return false;
            }

            if (!_fileService.IsImage(model.MainPhoto))
            {
                _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return false;
            }
            if (!_fileService.CheckSize(model.MainPhoto, 1800))
            {
                _modelState.AddModelError("MainPhoto", "File olcusu 300 kbdan boyukdur");
                return false;
            }



            var product = new ShopProduct
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
                ProductCategoryId = model.CategoryId,
                Price = model.Price,
                PhotoName= await _fileService.UploadAsync(model.MainPhoto)


            };

            await _shopProductRepository.CreateAsync(product);

            return true;
        }

        public async Task<ShopProductUpdateVM> GetUpdateModelAsync(int id)
        {


            var categories = await _productCategoryRepository.GetAllAsync();
            var product = await _shopProductRepository.GetAsync(id);

            if (product == null) return null;

            var model = new ShopProductUpdateVM
            {
                Id = product.Id,
                Categories = categories.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToList(),
                CategoryId = product.ProductCategoryId,

                Price = product.Price,
                Title = product.Title,
                MainPhotoPath = product.PhotoName,

            };

            return model;

        }

        public async Task<bool> UpdateAsync(ShopProductUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _shopProductRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda Product mövcuddur");
                return false;
            }
            if (model.MainPhoto != null)
            {
                if (!_fileService.IsImage(model.MainPhoto))
                {
                    _modelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.MainPhoto, 1800))
                {
                    _modelState.AddModelError("MainPhoto", "File olcusu 300 kbdan boyukdur");
                    return false;
                }
            }

            var product = await _shopProductRepository.GetAsync(model.Id);
            if (product != null)
            {
                product.Title = model.Title;
                product.ModifiedAt = DateTime.Now;
                product.Price = model.Price;
                product.ProductCategoryId = model.CategoryId;
                product.PhotoName = model.MainPhotoPath;

                if (model.MainPhoto != null)
                {
                    product.PhotoName = await _fileService.UploadAsync(model.MainPhoto);
                }
                await _shopProductRepository.UpdateAsync(product);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _shopProductRepository.GetAsync(id);
            if (product != null)
            {
                _fileService.Delete(product.PhotoName);
                await _shopProductRepository.DeleteAsync(product);

                return true;
            }
            return false;
        }
    }
}
