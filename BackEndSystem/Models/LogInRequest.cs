using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndSystem.Models
{
    public class LogInRequest
    {
        public string AdminAccount { get; set; }
        public string AdminPW { get; set; }
    }
}