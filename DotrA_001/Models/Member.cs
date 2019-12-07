namespace DotrA_001.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Member
    {
        public int MemberID { get; set; }
        [Display(Name = "帳號")]
        [Required(ErrorMessage = "必填欄位")]
        [StringLength(20)]
        public string MemberAccount { get; set; }
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "必填欄位")]
        //[StringLength(20)]
        public string Password { get; set; }
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "必填欄位")]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "信箱")]
        [DataType(DataType.EmailAddress)]
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
        public string HashCode { get; set; }//hash加密
        public bool EmailVerified { get; set; }
        public Guid ActivationCode { get; set; }
        public string ResetPasswordCode { get; set; }
    }
}
