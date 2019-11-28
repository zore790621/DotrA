using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndSystem.Models
{
    public class Member
    {
        public int MemberID { get; set; }

        public string MemberAccount { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}