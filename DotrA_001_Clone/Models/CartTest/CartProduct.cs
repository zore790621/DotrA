using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotrA_001_Clone.Models
{
    public class CartProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int ProductQuantity { get; set; }
        //商品小計
        public decimal Amount { get { return this.ProductPrice * this.ProductQuantity; } }
    }
}