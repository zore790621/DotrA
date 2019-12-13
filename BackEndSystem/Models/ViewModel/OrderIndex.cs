using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackEndSystem.Models.ViewModel
{
    public class OrderIndex
    {
       
        public int OrderID { get; set; }

        [Display(Name = "訂單日期")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "會員名稱")]
        public string MemberName { get; set; }

        [Display(Name = "運送管道")]
        public string ShipperName { get; set; }

        [Display(Name = "消費總金額")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "付款方式")]
        public string PaymentMethod { get; set; }
    }
}