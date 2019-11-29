using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotrA_001.Models
{
    public class View
    {
        public Members Member { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}