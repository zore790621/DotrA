using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndSystem.Models
{
    public class ECPayResult
    {
        public int? RtnCode { get; set; }

        public string MerchantTradeNo { get; set; }

        public string CustomField1 { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}