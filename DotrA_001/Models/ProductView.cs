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

        public int CategoryID { get; set; }
        public decimal UnitPrice { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
    }
}
