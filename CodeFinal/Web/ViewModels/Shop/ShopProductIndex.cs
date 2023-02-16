namespace Web.ViewModels.Shop
{
    public class ShopProductIndex
    {

        public Core.Entities.ProductCategory ProductCategory { get; set; }
        public List<Core.Entities.ShopProduct> ShopProducts { get; set; }
    }
}
