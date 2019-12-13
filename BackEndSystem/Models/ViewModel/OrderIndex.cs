using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndSystem.Models.ViewModel
{
    public class OrderIndex
    {
        public int OrderID { get; set; }

        public DateTime OrderDate { get; set; }

        public string MemberName { get; set; }

        public string ShipperName { get; set; }

        public decimal TotalPrice { get; set; }

        public string PaymentMethod { get; set; }
    }
}