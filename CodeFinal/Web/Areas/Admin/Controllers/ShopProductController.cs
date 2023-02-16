using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Product;
using Web.Areas.Admin.ViewModels.ShopProduct;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShopProductController : Controller
    {
        private readonly IShopProductService _productService;

        public ShopProductController(IShopProductService productService)
        {

            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _productService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _productService.GetCreateModelAsync();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Create(ShopProductCreateVM model)
        {
            var isSucceeded = await _productService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            model = await _productService.GetCreateModelAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _productService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ShopProductUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _productService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _productService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _productService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();


        }
    }
}
