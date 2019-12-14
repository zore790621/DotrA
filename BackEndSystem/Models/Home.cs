using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndSystem.Models
{
    public class Home
    {
        public int Prototal { get; set; }
        public int Cattotal { get; set; }
        public int Ordertotal { get; set; }
        public int Selltotal { get; set; }

        public int GetTotal()
        {
            return Prototal + Cattotal + Ordertotal + Selltotal;
        }
    }
}