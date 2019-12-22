using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotrA_001.Models
{
    public class ShopCartOrderView
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Picture { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientAddress { get; set; }
        public int ShipperID { get; set; }
        public int PaymentID { get; set; }
        //商品小記
        public decimal Total
        {
            get
            {
                return this.Price * this.Quantity;
            }
        }
    }
}