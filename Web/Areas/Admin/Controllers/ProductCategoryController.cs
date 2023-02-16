using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.ProductCategory;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {

            _productCategoryService = productCategoryService;
        }

        #region FaqCategory


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _productCategoryService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(ProductCategoryCreateVM model)
        {
            var isSucceeded = await _productCategoryService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _productCategoryService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductCategoryUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _productCategoryService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _productCategoryService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _productCategoryService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

            #endregion
        }
    }
}
