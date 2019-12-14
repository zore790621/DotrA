using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackEndSystem.Models.ViewModel
{
    public class OrderDetailVM
    {
        [Display(Name ="訂單編號")]
        public int OrderID { get; set; }

        public IEnumerable<OrderProductVM> OrderProducts { get; set; }
        [Display(Name = "收件人姓名")]
        public string RecipientName { get; set; }
        [Display(Name = "收件人電話")]
        public string RecipientPhone { get; set; }
        [Display(Name = "收件人地址")]
        public string RecipientAddress { get; set; }
    }

    public class OrderProductVM
    {
        [Display(Name="產品名稱")]
        public string ProductName { get; set; }
        [Display(Name = "數量")]
        public short Quantity { get; set; }
        [Display(Name = "折扣")]
        public float? Discount { get; set; }
        [Display(Name = "小計")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal SubTotal { get; set; }
        [Display(Name = "單價")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public int SalesPrice { get; set; }
    }
}