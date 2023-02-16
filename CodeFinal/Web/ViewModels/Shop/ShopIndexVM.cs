namespace Web.ViewModels.Shop
{
    public class ShopIndexVM
    {
        public List<Core.Entities.ShopProduct> ShopProducts { get; set; }
        public List<Core.Entities.ProductCategory> ProductCategories { get; set; }

        public string? Title { get; set; }
    }
}
