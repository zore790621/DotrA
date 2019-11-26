using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DotrA_001_Clone.Models
{
    public partial class Member
    {
        [Display(Name = "會員編號")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberID { get; set; }
        
        [Display(Name = "帳號")]
        [Required]
        [StringLength(20)]
        public string MemberAccount { get; set; }

        [Display(Name = "密碼")]
        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [Display(Name = "姓名")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "信箱")]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Display(Name = "地址")]
        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Display(Name = "行動電話")]
        [Required]
        [RegularExpression(@"^\d{4}\-?\d{3}\-?\d{3}$", ErrorMessage = "需為09xx-xxx-xxx格式")]
        [StringLength(20)]
        public string Phone { get; set; }

        //public virtual ICollection<AdminOrder> Orders { get; set; }
    }
}
