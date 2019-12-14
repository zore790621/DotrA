using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackEndSystem.Models.ViewModel
{
    public class LogInRequest
    {
        [Required]
        [Display(Name = "帳號")]
        public string AdminAccount { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string AdminPW { get; set; }
    }
}