using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotrA_001.Models
{
    public class ShopCartOrderView
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Picture { get; set; }
        [Display(Name = "收件人姓名")]
        public string RecipientName { get; set; }
        [Display(Name = "收件人電話")]
        public string RecipientPhone { get; set; }
        [Display(Name = "收件人地址")]
        public string RecipientAddress { get; set; }
        public int ShipperID { get; set; }
        public int PaymentID { get; set; }

        public string Description { get; set; }
    }
}