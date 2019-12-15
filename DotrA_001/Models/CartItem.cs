using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotrA_001.Models
{
    [Serializable]//可序列化
    public class CartItem//單一產品類別
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }



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