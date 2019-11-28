using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotrA_001.Models.ViewModel
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public decimal UnitPrice { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
    }
}