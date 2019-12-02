using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotrA_001.Models
{
    public class ShopView
    {
        public List<ProductView> Product { get; set; }
        public List<SupplierView> Supplier { get; set; }
        public List<CategoryView> Category { get; set; }
    }
}