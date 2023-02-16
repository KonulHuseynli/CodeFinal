using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ShopProduct :BaseEntity
    {

        public string Title { get; set; }
        public string PhotoName { get; set; }
        public double Price { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
