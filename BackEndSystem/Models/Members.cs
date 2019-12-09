namespace BackEndSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Members
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Members()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        [Display(Name = "ID")]
        public int MemberID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name="帳號")]
        public string MemberAccount { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "地址")]
        public string Address { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "電話")]
        public string Phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
