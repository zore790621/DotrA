using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotrA_001.Models.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "帳號")]
        [Required(ErrorMessage = "必填欄位")]
        [StringLength(20)]
        public string MemberAccount { get; set; }
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "必填欄位")]
        public string Password { get; set; }
        [Display(Name = "記住我")]
        public bool RememberMe { get; set; }
    }
}