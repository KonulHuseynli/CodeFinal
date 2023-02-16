namespace Web.Areas.Admin.ViewModels.ShopProduct
{
    public class ShopProductIndexVM
    {
        internal Core.Entities.ProductCategory ProductCategory;

        public List<Core.Entities.ShopProduct> Products { get; set; }
    }
}
