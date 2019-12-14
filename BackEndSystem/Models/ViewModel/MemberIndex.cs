using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackEndSystem.Models.ViewModel
{
    public class MemberIndex
    {
        [Display(Name ="ID")]
        public int MemberID { get; set; }

        [Display(Name = "帳號")]
        public string MemberAccount { get; set; }

  
        [Display(Name = "姓名")]
        public string MemberName { get; set; }

        [Display(Name = "信箱")]
        public string Email { get; set; }

        [Display(Name = "地址")]
        public string Address { get; set; }

        [Display(Name = "電話")]
        public string Phone { get; set; }

    }
}