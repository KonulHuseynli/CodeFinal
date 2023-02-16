using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.ViewModels.ShopProduct
{
    public class ShopProductCreateVM
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public IFormFile MainPhoto { get; set; }
    }
}
