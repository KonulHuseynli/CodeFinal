using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Shop;

namespace Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {

            _shopService = shopService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ShopIndexVM model)
        {
            model = await _shopService.GetAllAsync(model);
            return View(model);
        }

        public async Task<IActionResult> CategoryProduct(int id)
        {
            var model = await _shopService.CategoryProductAsync(id);

            return PartialView("_ProductPartial", model);

        }
    }
}
