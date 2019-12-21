using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotrA_001.Models
{
    public class ECPayModel
    {
        public int RtnCode { get; set; }

        public string RtnMsg { get; set; }

        public string PaymentType { get; set; }

        public string TradeNo { get; set; }

        public string PaymentDate { get; set; }

        public int TradeAmt { get; set; }
    }
}