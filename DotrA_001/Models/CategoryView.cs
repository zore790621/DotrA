using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DotrA_001.Models
{
    public partial class CategoryView
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Picture { get; set; }
    }
}
