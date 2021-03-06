using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DotrA_001.Models
{
    public partial class ProductView
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }

        public int SupplierID { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }    
        public string Picture { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public decimal SalesPrice { get; set; }

    }
}
