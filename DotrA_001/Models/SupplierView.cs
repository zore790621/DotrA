using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DotrA_001.Models
{
    public partial class SupplierView
    {
        public int SupplierID { get; set; }
        public string CompanyName { get; set; }
        public string CampanyPhone { get; set; }
        public string CompanyAddress { get; set; }
    }
}
