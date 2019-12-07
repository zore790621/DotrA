using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotrA_001.Models.ViewModels
{
    public class ResetPasswordVM
    {
        [Display(Name = "新密碼")]
        [Required(ErrorMessage = "New password required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "確認密碼")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "New password and confirm password does not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}