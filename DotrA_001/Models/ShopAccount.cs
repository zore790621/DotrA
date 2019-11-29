using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotrA_001.Models
{
    public class ShopAccount
    {
        public int MemberID { get; set; }
        [Display(Name = "帳號")]
        [Required(ErrorMessage = "必填欄位")]
        [StringLength(20)]
        public string MemberAccount { get; set; }
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "必填欄位")]
        [StringLength(20)]
        public string Password { get; set; }
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "必填欄位")]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "信箱")]
        //[DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "必填欄位")]
        [StringLength(50)]
        public string Email { get; set; }
        [Display(Name = "地址")]
        [Required(ErrorMessage = "必填欄位")]
        [StringLength(50)]
        public string Address { get; set; }
        [Display(Name = "電話")]
        [Required(ErrorMessage = "必填欄位")]
        [StringLength(20)]
        public string Phone { get; set; }
    }
}