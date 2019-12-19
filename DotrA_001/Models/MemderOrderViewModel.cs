using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotrA_001.Models
{
    public class MemderOrderViewModel
    {
        public string RecipientName { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientAddress { get; set; }
        public int ShipperID { get; set; }
        public int PaymentID { get; set; }
        public string Picture { get; set; }

    }
}